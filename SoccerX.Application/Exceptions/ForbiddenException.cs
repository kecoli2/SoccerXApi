using System.Net;

namespace SoccerX.Application.Exceptions;
public class ForbiddenException : BaseException
{
    #region Field
    #endregion

    #region Constructor
    public ForbiddenException(string message = "Forbidden")
        : base(message, HttpStatusCode.Forbidden) { }
    #endregion

    #region Public Method
    #endregion

    #region Private Method
    #endregion
}
