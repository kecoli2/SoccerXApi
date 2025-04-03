using SoccerX.Domain.Enums;

namespace SoccerX.Domain.Entities
{
    public partial class User
    {
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }
    }
}
