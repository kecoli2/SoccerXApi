using MediatR;
using SoccerX.Application.Commands.UserCommand;
using SoccerX.Application.Services.CustomerService;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerX.Application.Handler.UserHandler
{
    public class UserResendVerifyEmailCodeHandler : IRequestHandler<UserResendVerifyEmailCodeCommand, bool>
    {
        #region Field
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public UserResendVerifyEmailCodeHandler(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Public Method
        public async Task<bool> Handle(UserResendVerifyEmailCodeCommand request, CancellationToken cancellationToken)
        {
            await _userService.SendRenewVerificationCode(cancellationToken);
            return true;
        }
        #endregion

        #region Private Method
        #endregion        
    }
}
