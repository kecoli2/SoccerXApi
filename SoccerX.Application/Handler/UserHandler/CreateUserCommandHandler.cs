using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SoccerX.Application.Commands.UserCommand;
using SoccerX.Application.Services.CustomerService;

namespace SoccerX.Application.Handler.UserHandler
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        #region Field
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Public Method
        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.CreateUser(request.userCreateDto, cancellationToken);
            return true;
        }
        #endregion

        #region Private Method
        #endregion
    }
}
