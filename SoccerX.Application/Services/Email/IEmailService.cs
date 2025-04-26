using System.Threading.Tasks;

namespace SoccerX.Application.Services.Email
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body);
        public Task SendHtmlEmailAsync(string to, string subject, string htmlContent);
    }
}
