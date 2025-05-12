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
        public async Task<bool> Handle(UserBalaceChangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region Private Method
        #endregion        
    }
}
