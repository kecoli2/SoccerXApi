using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Application.Exceptions;
public class NotFoundException : BaseException
{
    #region Field
    #endregion

    #region Constructor
    public NotFoundException(string message)
        : base(message, HttpStatusCode.NotFound) { }
    #endregion

    #region Public Method
    #endregion

    #region Private Method
    #endregion
}
