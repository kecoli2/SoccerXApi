using System.Threading.Tasks;

namespace SoccerX.Application.Services.Email
{
    public interface IEmailService
    {
        public Task<string?> SendEmailAsync(string to, string subject, string body);
        public Task<string?> SendHtmlEmailAsync(string to, string subject, string htmlContent);
    }
}
