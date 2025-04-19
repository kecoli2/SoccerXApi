using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.DTO.Responses
{
    public class AuthResponseDto
    {
        #region Field
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }      // saniye
        public bool IsNewUser { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
