using System.Net;

namespace SoccerX.Application.Exceptions;
public class NotFoundException : BaseException
{
    #region Field
    #endregion

    #region Constructor
    public NotFoundException(string message): base(message, HttpStatusCode.NotFound) { }
    #endregion

    #region Public Method
    #endregion

    #region Private Method
    #endregion
}
