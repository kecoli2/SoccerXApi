using MailKit.Net.Smtp;
using SoccerX.Application.Services.Email;
using SoccerX.Common.Configuration;
using MimeKit;

namespace SoccerX.Infrastructure.Services.Email
{
    public class EmailService: IEmailService
    {
        #region Field
        private readonly SmtpSettings _smtpSettings;
        #endregion

        #region Constructor
        public EmailService(ApplicationSettings applicationSettings)
        {
            _smtpSettings = applicationSettings.SmtpSettings;
        }
        #endregion

        #region Public Method
        public async Task<string?> SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.FromName, _smtpSettings.FromEmail));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, _smtpSettings.UseSsl);
            await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            var result = await client.SendAsync(message);
            await client.DisconnectAsync(true);
            return result.ToString();
        }

        public async Task<string?> SendHtmlEmailAsync(string to, string subject, string htmlContent)
        {
            var message = new MimeMessage();

            // Gönderici bilgileri
            message.From.Add(new MailboxAddress(_smtpSettings.FromName, _smtpSettings.FromEmail));
            // Alıcı bilgileri
            message.To.Add(MailboxAddress.Parse(to));
            // Konu
            message.Subject = subject;

            // HTML body oluşturma
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlContent,
                // Alternatif plain text versiyon (isteğe bağlı)
                TextBody = StripHtml(htmlContent)
            };

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                // SMTP bağlantısı
                await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, _smtpSettings.UseSsl);
                // Kimlik doğrulama
                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                // Email gönderme
                var result = await client.SendAsync(message);
                return result.ToString();
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }

        #endregion

        #region Private Method
        private string StripHtml(string html)
        {
            // Basit bir HTML temizleme metodu
            return System.Text.RegularExpressions.Regex.Replace(
                html,
                "<[^>]*>",
                string.Empty);
        }
        #endregion
    }
}
