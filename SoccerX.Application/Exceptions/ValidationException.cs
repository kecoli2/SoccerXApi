using System.Collections.Generic;
using System.Net;

namespace SoccerX.Application.Exceptions;
public class ValidationException : BaseException
{
    #region Field
    #endregion

    #region Constructor
    public ValidationException(Dictionary<string, string[]> errors) : base(errors)
    {
        StatusCode = HttpStatusCode.BadRequest;
    }
    #endregion

    #region Public Method
    #endregion

    #region Private Method
    #endregion
}
