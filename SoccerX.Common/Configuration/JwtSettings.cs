using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Common.Configuration
{
    /// <summary>
    /// JWT authentication settings used to generate and validate tokens.
    /// </summary>
    public class JwtSettings
    {
        #region Field
        /// <summary>
        /// Token issuer (e.g. your domain or application name).
        /// </summary>
        public string Issuer { get; set; } = "SoccerX";

        /// <summary>
        /// Token audience (target users or clients).
        /// </summary>
        public string Audience { get; set; } = "SoccerXUsers";

        /// <summary>
        /// Symmetric encryption key used for signing (32 bytes recommended).
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// Token expiration time in minutes (e.g. 30).
        /// </summary>
        public int ExpirationMinutes { get; set; } = 30;

        /// <summary>
        /// Token renewal threshold in minutes (e.g. renew if < 5 minutes left).
        /// </summary>
        public int RenewalThresholdMinutes { get; set; } = 5;
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
