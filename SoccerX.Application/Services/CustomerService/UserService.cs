using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using SoccerX.Application.Exceptions;
using SoccerX.Application.Interfaces.Quartz;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Application.Interfaces.Resources;
using SoccerX.Application.Services.CountryService;
using SoccerX.Common.Base.Quartz.Criteria;
using SoccerX.Common.Enums;
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
        #endregion

        #region Constructor
        public UserService(ICountriesService countriesService, 
            IResourceManager resourceManager, IMapper mapper, IUnitOfWork unitOfWork, IQuartzJobCreater quartzJobCreater, IQuartzJobCreaterExtension quartzJobCreaterExtension)
        {
            _countriesService = countriesService;
            _resourceManager = resourceManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _quartzJobCreater = quartzJobCreater;
            _quartzJobCreaterExtension = quartzJobCreaterExtension;
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
                var isExistEmail = await _unitOfWork.UserRepository.CountAsync(u => u.Email == userDto.Name) > 0;
                if (isExistEmail)
                {
                    throw new ValidationException(new Dictionary<string, string[]> { { "Email", new[] { userDto.Email } } },
                        string.Format(_resourceManager.GetString("error_userEmailIsExist"), userDto.Email));
                }
                var isUserName = await _unitOfWork.UserRepository.CountAsync(u => u.Username == userDto.Username) > 0;
                if (isUserName)
                {
                    throw new ValidationException(new Dictionary<string, string[]> { { "Username", new[] { userDto.Username } } }, string.Format(_resourceManager.GetString("error_userNameIsExist"), userDto.Username));
                }

                var user = _mapper.Map<User>(userDto);
                await _unitOfWork.UserRepository.AddAsync(user);                
                await _unitOfWork.CommitTransactionAsync();
                await _quartzJobCreater.Create(JobKeyEnum.SendVerificationMail)
                    .SetCriteria(new SendEmailVerifcationCriteria
                    {
                        UserId = user.Id.ToString(),
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

        #endregion

        #region Private Method
        #endregion
    }
}
