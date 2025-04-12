using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Application.Exceptions
{
    public class DatabaseConstraintException: BaseException
    {
        #region Field
        #endregion

        #region Constructor
        public DatabaseConstraintException(string message) : base(message)
        {
        }

        public DatabaseConstraintException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {
        }

        public DatabaseConstraintException(Dictionary<string, string[]> propertyErrors) : base(propertyErrors)
        {
        }
        public DatabaseConstraintException(Dictionary<string, string[]> propertyErrors, string message) : base(propertyErrors, message)
        {

        }


        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
