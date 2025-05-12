using SoccerX.Domain.Enums;

namespace SoccerX.Domain.Entities
{
    public partial class User
    {
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }
        public UserGender Gender { get; set; }
        public AuthProvider Provider { get; set; }
        public uint Xmin { get; set; }
    }
}
