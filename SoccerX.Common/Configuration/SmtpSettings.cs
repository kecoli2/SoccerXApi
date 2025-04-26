namespace SoccerX.Common.Configuration
{
    /// <summary>
    /// SMTP settings for outgoing emails.
    /// </summary>
    public class SmtpSettings
    {
        #region Field
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 587;
        public bool UseSsl { get; set; } = true;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string FromName { get; set; } = "SoccerX";
        #endregion
    }
}
