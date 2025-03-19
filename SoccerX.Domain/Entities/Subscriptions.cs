using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class Subscriptions
{
    public Guid Id { get; set; }

    public Guid Subscriberid { get; set; }

    public Guid Editorid { get; set; }

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }

    public bool Isactive { get; set; }

    public DateTime? Createdate { get; set; }

    public DateTime? Updatedate { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual Users Editor { get; set; } = null!;

    public virtual Users Subscriber { get; set; } = null!;
}
