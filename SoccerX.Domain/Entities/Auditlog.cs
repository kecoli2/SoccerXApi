using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class Auditlog
{
    public Guid Id { get; set; }

    public string Entityname { get; set; } = null!;

    public Guid Entityid { get; set; }

    public Guid Performedby { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? Details { get; set; }

    public DateTime? Createdate { get; set; }

    public DateTime? Updatedate { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual Users PerformedbyNavigation { get; set; } = null!;
}
