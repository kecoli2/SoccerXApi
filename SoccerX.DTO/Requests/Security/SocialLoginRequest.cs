using SoccerX.Domain.Enums;

namespace SoccerX.DTO.Requests.Security
{
    public class SocialLoginRequest
    {
        public string IdToken { get; set; } = null!;
        public AuthProvider Provider { get; set; }

    }
}
