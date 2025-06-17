using MediatR;
using SoccerX.Application.Commands.UserCommand;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerX.Application.Handler.UserHandler
{
    public class UserBalaceChangeHandler : IRequestHandler<UserBalaceChangeCommand, bool>
    {
        #region Field
        #endregion
        #region Constructor
        #endregion
        #region Public Method
        public Task<bool> Handle(UserBalaceChangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
        #endregion
        #region Private Method
        #endregion        
    }
}
