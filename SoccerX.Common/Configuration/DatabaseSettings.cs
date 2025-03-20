using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Common.Configuration
{
    /// <summary>
    /// PostgreSQL connection settings.
    /// </summary>
    public class DatabaseSettings
    {
        #region Fields
        /// <summary>
        /// PostgreSQL connection string.
        /// </summary>
        public string ConnectionString { get; set; } = "";

        /// <summary>
        /// Maximum connection pool size.
        /// </summary>
        public int MaxPoolSize { get; set; } = 100;

        /// <summary>
        /// Minimum connection pool size.
        /// </summary>
        public int MinPoolSize { get; set; } = 10;

        /// <summary>
        /// Determines whether SSL should be used for PostgreSQL connection.
        /// </summary>
        public bool UseSsl { get; set; } = false;

        /// <summary>
        /// Timeout duration for the database connection (in seconds).
        /// </summary>
        public int ConnectionTimeout { get; set; } = 30;
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
