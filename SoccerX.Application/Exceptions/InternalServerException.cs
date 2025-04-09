using System.Net;

namespace SoccerX.Application.Exceptions;

public class InternalServerException: BaseException
{
    #region Field
    #endregion

    #region Constructor
    public InternalServerException(string message = "Unexpected server error")
        : base(message, HttpStatusCode.InternalServerError) { }
    #endregion

    #region Public Method
    #endregion

    #region Private Method
    #endregion
}