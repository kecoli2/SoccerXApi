using MediatR;
using SoccerX.Application.Commands.Security;
using SoccerX.DTO.Responses;
using System;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using SoccerX.Application.Exceptions;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Application.Interfaces.Security;
using SoccerX.Common.Extensions;
using SoccerX.Common.Helpers;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;
using System.Linq.Expressions;
using SoccerX.Application.Interfaces.Resources;

namespace SoccerX.Application.Handler.Security
{
    public class LocalLoginCommandHandler : IRequestHandler<LocalLoginCommand, AuthResponseDto>
    {
        #region Field
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IResourceManager _resourceManager;
        #endregion

        #region Constructor
        public LocalLoginCommandHandler(IUserRepository userRepository, ITokenService tokenService, IResourceManager resourceManager)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _resourceManager = resourceManager;
        }
        #endregion

        #region Public Method
        public async Task<AuthResponseDto> Handle(LocalLoginCommand request, CancellationToken cancellationToken)
        {
            var isEmail = request.EmailUserName.IsValidEmail();
            Expression<Func<User, User>>? selector = u => new User
            {
                Id = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                Username = u.Username,
                Passwordhash = u.Passwordhash,
                Banenddate = u.Banenddate,
                Status = u.Status,
                Role = u.Role
            };
            User? user = null;

            if (isEmail)
            {
                var users = await _userRepository.FindAsync(u => u.Email == request.EmailUserName, selector: selector ,cancellationToken: cancellationToken);
                user = users.FirstOrDefault();
            }
            else
            {
                var users = await _userRepository.FindAsync(u => u.Username == request.EmailUserName, selector: selector, cancellationToken: cancellationToken);
                user = users.FirstOrDefault();
            }

            if (user == null)
            {
                throw new NotFoundException( _resourceManager.GetString("error_InvalidUserNamePassword"));
            }

            if (user.Passwordhash.Decrypt() != request.Password)
                throw new NotFoundException(_resourceManager.GetString("error_InvalidUserNamePassword"));

            switch (user.Status)
            {
                case UserStatus.Banned when user.Banenddate >= DateTime.Now:
                    throw new UnauthorizedException(_resourceManager.GetString("error_userBanned", user.Banenddate?.ToString("dd/MM/yyyy HH:mm")!));
                case UserStatus.Banned:
                    await _userRepository.UpdateUserStatus(user.Id, UserStatus.Active);
                    break;
                case UserStatus.Active:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // 3) JWT + Refresh token üret
            var authDto = _tokenService.GenerateTokens(user.Id, user.Role, request.Platform);
            authDto.IsNewUser = false;
            authDto.Name = user.Name;
            authDto.SurName = user.Surname;
            authDto.Email = user.Email;
            return authDto;
        }
        #endregion

        #region Private Method
        #endregion
    }
}
