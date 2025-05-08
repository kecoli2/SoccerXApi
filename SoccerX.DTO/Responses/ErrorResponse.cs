using System.Collections.Generic;

namespace SoccerX.DTO.Responses
{
    public class ErrorResponse
    {
        #region Field
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public Dictionary<string, string[]>? PropertyErrors { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
