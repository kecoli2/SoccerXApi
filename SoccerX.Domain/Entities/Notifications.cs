using System;
using System.Collections.Generic;

namespace SoccerX.Infrastructure;

public partial class Notifications
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public string Message { get; set; } = null!;

    public bool Isread { get; set; }

    public DateTime? Createdate { get; set; }

    public DateTime? Updatedate { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual Users User { get; set; } = null!;
}
