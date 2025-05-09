using MediatR;
using SoccerX.Application.Commands.Security;
using SoccerX.Application.Services.CustomerService;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerX.Application.Handler.UserHandler
{
    public class ChangeUserPasswordHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        #region Field
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public ChangeUserPasswordHandler(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Public Method
        public Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            return _userService.ChangeCurrentPassword(request.securityKey, request.oldPassword, request.newPassword, cancellationToken);
        }
        #endregion

        #region Private Method
        #endregion
    }
}
