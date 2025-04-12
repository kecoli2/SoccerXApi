using System;
using System.Collections.Generic;
using System.Net;

namespace SoccerX.Application.Exceptions;

/// <summary>
/// Base exception class for handling domain-specific and validation-related errors.
/// </summary>
public class BaseException : Exception
{
    #region Field
    /// <summary>
    /// HTTP status code for the exception (default: 400 BadRequest).
    /// </summary>
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;

    /// <summary>
    /// Dictionary of property-specific validation errors.
    /// Key: Property name, Value: Error message(s)
    /// </summary>
    public Dictionary<string, string[]>? PropertyErrors { get; set; }
    #endregion

    #region Constructor
    public BaseException(string message) : base(message) { }

    public BaseException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public BaseException(Dictionary<string, string[]> propertyErrors) : base("One or more validation errors occurred.")
    {
        PropertyErrors = propertyErrors;
    }

    public BaseException(Dictionary<string, string[]> propertyErrors, string message) : base(message)
    {
        PropertyErrors = propertyErrors;
    }
    #endregion

    #region Public Method
    #endregion

    #region Private Method
    #endregion
}
