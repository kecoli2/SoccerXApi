using System.Net;

namespace SoccerX.Application.Exceptions;
public class UnauthorizedException : BaseException
{
    #region Field
    #endregion

    #region Constructor
    public UnauthorizedException(string message = "Unauthorized"): base(message, HttpStatusCode.Unauthorized) { }
    #endregion

    #region Public Method
    #endregion

    #region Private Method
    #endregion
}
