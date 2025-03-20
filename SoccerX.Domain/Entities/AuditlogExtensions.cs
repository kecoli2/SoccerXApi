using SoccerX.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Domain.Entities
{
    public partial class Auditlog
    {
        public AuditAction Action { get; set; }
    }
}
