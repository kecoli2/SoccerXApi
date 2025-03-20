using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Common.Configuration
{
    public class ApplicationSettings
    {
        #region Fields
        /// <summary>
        /// PostgreSQL database connection settings.
        /// </summary>
        public DatabaseSettings Database { get; set; } = new DatabaseSettings();

        /// <summary>
        /// Redis configuration settings.
        /// </summary>
        public RedisSettings Redis { get; set; } = new RedisSettings();

        /// <summary>
        /// Quartz job scheduling settings.
        /// </summary>
        public QuartzSettings Quartz { get; set; } = new QuartzSettings();

        /// <summary>
        /// IP rate limiting settings.
        /// </summary>
        public RateLimitSettings RateLimit { get; set; } = new RateLimitSettings();
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
