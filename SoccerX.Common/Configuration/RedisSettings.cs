using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Common.Configuration
{
    /// <summary>
    /// Configuration settings for Redis, supporting both standard and Sentinel-based connections.
    /// </summary>
    public class RedisSettings
    {
        #region Fields
        /// <summary>
        /// Indicates whether Redis Sentinel is used.
        /// If true, the application will connect to a Sentinel-managed Redis cluster.
        /// </summary>
        public bool UseSentinel { get; set; } = false;

        /// <summary>
        /// List of Redis Sentinel hosts in "IP:Port" format.
        /// Used only when UseSentinel is true.
        /// </summary>
        public string[] SentinelHosts { get; set; } = Array.Empty<string>();

        /// <summary>
        /// The master name used by Redis Sentinel.
        /// This is required for identifying the master Redis instance in a Sentinel setup.
        /// </summary>
        public string MasterName { get; set; } = "mymaster";

        /// <summary>
        /// Redis host address for standard connections.
        /// Ignored if UseSentinel is true.
        /// </summary>
        public string Host { get; set; } = "localhost";

        /// <summary>
        /// Redis port for standard connections.
        /// Ignored if UseSentinel is true.
        /// Default is 6379.
        /// </summary>
        public int Port { get; set; } = 6379;

        /// <summary>
        /// Password for Redis authentication.
        /// If empty, authentication is disabled.
        /// </summary>
        public string Password { get; set; } = "";

        /// <summary>
        /// Specifies which Redis database to use.
        /// Default is 0.
        /// </summary>
        public int Database { get; set; } = 0;

        /// <summary>
        /// Determines whether to use SSL for the Redis connection.
        /// Default is false.
        /// </summary>
        public bool UseSsl { get; set; } = false;
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
