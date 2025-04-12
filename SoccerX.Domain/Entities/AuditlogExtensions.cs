using SoccerX.Domain.Enums;

namespace SoccerX.Domain.Entities
{
    public partial class Auditlog
    {
        public AuditAction Action { get; set; }
    }
}
