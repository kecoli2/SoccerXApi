namespace SoccerX.Common.Configuration
{
    /// <summary>
    /// PostgreSQL connection settings.
    /// </summary>
    public class DatabaseSettings
    {
        #region Fields
        /// <summary>
        /// PostgreSQL host (server address).
        /// </summary>
        public string Host { get; set; } = "localhost";

        /// <summary>
        /// PostgreSQL connection port.
        /// Default: 5432.
        /// </summary>
        public int Port { get; set; } = 5432;

        /// <summary>
        /// PostgreSQL database name.
        /// </summary>
        public string DatabaseName { get; set; } = "soccerxdb";

        /// <summary>
        /// PostgreSQL username.
        /// </summary>
        public string Username { get; set; } = "postgres";

        /// <summary>
        /// PostgreSQL password.
        /// </summary>
        public string Password { get; set; } = "kecoli2";

        /// <summary>
        /// Maximum connection pool size.
        /// Default: 100.
        /// </summary>
        public int MaxPoolSize { get; set; } = 100;

        /// <summary>
        /// Minimum connection pool size.
        /// Default: 10.
        /// </summary>
        public int MinPoolSize { get; set; } = 10;

        /// <summary>
        /// Connection timeout in seconds.
        /// Default: 30.
        /// </summary>
        public int ConnectionTimeout { get; set; } = 30;

        /// <summary>
        /// Command execution timeout in seconds.
        /// Default: 60.
        /// </summary>
        public int CommandTimeout { get; set; } = 60;

        /// <summary>
        /// Whether SSL is required for the connection.
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
