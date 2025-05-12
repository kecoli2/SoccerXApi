namespace SoccerX.Common.Configuration
{
    public class RetrySettings
    {
        #region Field
        public int MaxRetryAttempts { get; set; } = 3;
        public int InitialBackoffMs { get; set; } = 200;    // 200ms
        public double BackoffExponent { get; set; } = 2.0;    // 200ms, 400ms, 800ms...
        public bool HandleAllExceptions { get; set; } = true;   // tüm exceptionları retry et
        public Type HandledExceptionType { get; set; } = typeof(Exception);
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
