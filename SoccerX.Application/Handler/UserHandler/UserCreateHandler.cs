using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SoccerX.Application.Commands.UserCommand;
using SoccerX.Application.Services.CustomerService;

namespace SoccerX.Application.Handler.UserHandler
{
    public class UserCreateHandler : IRequestHandler<UserCreateCommand, bool>
    {
        #region Field
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public UserCreateHandler(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Public Method
        public async Task<bool> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            await _userService.CreateUser(request.userCreateDtoRequest, cancellationToken);
            return true;
        }
        #endregion

        #region Private Method
        #endregion
    }
}
