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

        /// <summary>
        /// JWT Settings
        /// </summary>
        public JwtSettings JwtSettings { get; set; } = new JwtSettings();

        public string AppleClientId { get; set; } = "";
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        /// <summary>
        /// Generates a PostgreSQL connection string based on provided settings.
        /// </summary>        
        /// <returns>Formatted PostgreSQL connection string.</returns>

        public string GetDatabaseConnectionString()
        {
            if (Database == null)
                throw new ArgumentNullException(nameof(Database), "Database settings cannot be null.");

            return $"Host={Database.Host};" +
                   $"Port={Database.Port};" +
                   $"Database={Database.DatabaseName};" +
                   $"Username={Database.Username};" +
                   $"Password={Database.Password};" +
            $"Pooling=true;" +
                   $"MinPoolSize={Database.MinPoolSize};" +
                   $"MaxPoolSize={Database.MaxPoolSize};" +
                   $"Timeout={Database.ConnectionTimeout};" +
                   $"CommandTimeout={Database.CommandTimeout};" +
                   $"SslMode={(Database.UseSsl ? "Require" : "Disable")};";
        }
        #endregion

        #region Private Methods
        #endregion

        #region Protected
        #endregion
    }
}


