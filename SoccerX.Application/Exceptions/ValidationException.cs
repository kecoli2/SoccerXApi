using System.Collections.Generic;
using System.Net;

namespace SoccerX.Application.Exceptions;
public class ValidationException : BaseException
{
    #region Field
    #endregion

    #region Constructor
    public ValidationException(Dictionary<string, string[]> errors, string message) : base(errors, message)
    {
        StatusCode = HttpStatusCode.BadRequest;
    }
    #endregion

    #region Public Method
    #endregion

    #region Private Method
    #endregion
}
