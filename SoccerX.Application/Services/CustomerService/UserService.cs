using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SoccerX.Application.Exceptions;
using SoccerX.Application.Interfaces.Quartz;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Application.Interfaces.Resources;
using SoccerX.Application.Services.CountryService;
using SoccerX.Common.Base.Quartz.Criteria;
using SoccerX.Common.Enums;
using SoccerX.Common.Properties;
using SoccerX.Domain.Entities;
using SoccerX.DTO.Dto.User;

namespace SoccerX.Application.Services.CustomerService
{
    public class UserService: IUserService
    {
        #region Field
        private readonly ICountriesService _countriesService;
        private readonly IResourceManager _resourceManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuartzJobCreater _quartzJobCreater;
        private readonly IQuartzJobCreaterExtension _quartzJobCreaterExtension;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public UserService(ICountriesService countriesService,
            IResourceManager resourceManager, IMapper mapper, IUnitOfWork unitOfWork, IQuartzJobCreater quartzJobCreater, IQuartzJobCreaterExtension quartzJobCreaterExtension, IHttpContextAccessor httpContextAccessor)
        {
            _countriesService = countriesService;
            _resourceManager = resourceManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _quartzJobCreater = quartzJobCreater;
            _quartzJobCreaterExtension = quartzJobCreaterExtension;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Public Method
        public async Task NewCustomerSocial(User user, CancellationToken cancellationToken = default)
        {
            user.Countryid = (await _countriesService.GetOtherCountry()).Id;
            user.Cityid = (await _countriesService.GetCities(user.Countryid)).First().Id;
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task CreateUser(UserCreateDto userDto, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var isExistEmail = await _unitOfWork.UserRepository.CountAsync(u => u.Email == userDto.Email) > 0;
                if (isExistEmail)
                {
                    throw new ValidationException(new Dictionary<string, string[]> { { "Email", new[] { userDto.Email } } },
                        string.Format(Resources.error_userEmailIsExist, userDto.Email));
                }
                var isUserName = await _unitOfWork.UserRepository.CountAsync(u => u.Username == userDto.Username) > 0;
                if (isUserName)
                {
                    throw new ValidationException(new Dictionary<string, string[]> { { "Username", new[] { userDto.Username } } }, string.Format(Resources.error_userNameIsExist, userDto.Username));
                }

                var isPhoneNumber = await _unitOfWork.UserRepository.CountAsync(u => u.Phonenumber == userDto.Phonenumber) > 0;
                if (isPhoneNumber)
                {
                    throw new ValidationException(new Dictionary<string, string[]> { { "Phonenumber", new[] { userDto.Phonenumber } } }, Resources.error_userPhoneNumberIsExist);
                }

                var user = _mapper.Map<User>(userDto);
                user.Createdate = DateTime.Now;
                user.Status = Domain.Enums.UserStatus.WaitingForEmailVerification;

                await _unitOfWork.UserRepository.AddAsync(user);                
                await _unitOfWork.CommitTransactionAsync();
                await _quartzJobCreater.Create(JobKeyEnum.SendVerificationMail)
                    .SetCriteria(new SendEmailVerifcationCriteria
                    {
                        UserId = user.Id,
                        ToMailAddress = user.Email,
                    })
                    .SetPriority(TriggerPriorityEnum.High)
                    .SetCulture(_resourceManager.GetCultureKey())
                    .SetUserId(user.Id)
                    .Start();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

        }

        public async Task VerifiedUser(string code, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            var userId = await GetActiveUserId();
            try
            {
                Expression<Func<User, User>>? selector = u => new User
                {
                    Id = u.Id,
                    Email = u.Email,
                    Isemailconfirmed = u.Isemailconfirmed,
                };

                var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, selector);
                if (user == null)
                {
                    throw new BaseException(Resources.error_userNotFound);
                }
                var emailVerification = _unitOfWork.EmailVerificationRepository.GetAllAsync(x => x.Userid == userId && x.Code == code
                && x.Isused == false && x.Expiresat >= DateTime.Now).Result.FirstOrDefault();
                if (emailVerification == null)
                {
                    throw new BaseException(Resources.error_userEmailVerificationCodeNotFound);
                }
                user.Isemailconfirmed = true;
                emailVerification.Isused = true;
                _unitOfWork.UserRepository.Update(user);
                _unitOfWork.EmailVerificationRepository.Update(emailVerification);
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task SendRenewVerificationCode(CancellationToken cancellationToken)
        {
            var userId = await GetActiveUserId();
            Expression<Func<User, User>>? selector = u => new User
            {
                Id = u.Id,
                Email = u.Email
            };

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, selector);
            if(user == null)
            {
                throw new BaseException(Resources.error_userNotFound);
            }

            await _quartzJobCreater.Create(JobKeyEnum.SendVerificationMail)
                    .SetCriteria(new SendEmailVerifcationCriteria
                    {
                        UserId = user.Id,
                        ToMailAddress = user.Email,
                    })
                    .SetPriority(TriggerPriorityEnum.High)
                    .SetCulture(_resourceManager.GetCultureKey())
                    .SetUserId(user.Id)
                    .Start();
        }

        public Task<Guid> GetActiveUserId()
        {
            var claim = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (claim == null)
            {
                throw new UnauthorizedException();
            }
            var userId = Guid.Parse(claim);
            return Task.FromResult(userId);
        }
        #endregion

        #region Private Method
        #endregion
    }
}
