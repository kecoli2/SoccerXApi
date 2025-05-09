using MediatR;
using SoccerX.Application.Commands.UserCommand;
using SoccerX.Application.Services.CustomerService;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerX.Application.Handler.UserHandler
{
    public class UserVerifyEmailHandler : IRequestHandler<UserVerifyEmailCommand, bool>
    {
        #region Field
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public UserVerifyEmailHandler(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Public Method
        public async Task<bool> Handle(UserVerifyEmailCommand request, CancellationToken cancellationToken)
        {
            await _userService.VerifiedUser(request.code, cancellationToken);
            return true;
        }
        #endregion

        #region Private Method
        #endregion        
    }
}
