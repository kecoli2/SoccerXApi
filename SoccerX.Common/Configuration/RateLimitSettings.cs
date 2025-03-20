using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace SoccerX.Common.Configuration
{
    /// <summary>
    /// Configuration settings for IP rate limiting.
    /// This configuration controls request rate limiting based on IP addresses.
    /// </summary>
    public class RateLimitSettings
    {
        #region Fields
        /// <summary>
        /// Indicates whether IP rate limiting is enabled.
        /// If false, no rate limiting will be applied.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Defines the maximum number of requests allowed within the specified time period.
        /// </summary>
        public int Limit { get; set; } = 100;

        /// <summary>
        /// The duration (in seconds) for the rate limiting window.
        /// Example: If Limit = 100 and Period = 60, it allows 100 requests per minute.
        /// </summary>
        public int PeriodInSeconds { get; set; } = 60;

        /// <summary>
        /// A list of whitelisted IP addresses that are exempt from rate limiting.
        /// </summary>
        public string[] WhitelistedIPs { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Determines whether to block a request when the limit is exceeded.
        /// If false, it will return a "429 Too Many Requests" response.
        /// </summary>
        public bool BlockOnLimit { get; set; } = true;

        /// <summary>
        /// The response message returned when a request exceeds the rate limit.
        /// </summary>
        public string LimitExceededMessage { get; set; } = "Too many requests. Please try again later.";

        /// <summary>
        /// Whether rate limiting applies globally or per IP address.
        /// If true, it limits total requests globally rather than per IP.
        /// </summary>
        public bool GlobalLimit { get; set; } = false;
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        #region Protected
        #endregion
    }
}

