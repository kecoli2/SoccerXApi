using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class Comments
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public Guid Betslipid { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? Createdate { get; set; }

    public DateTime? Updatedate { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual Betslips Betslip { get; set; } = null!;

    public virtual Users User { get; set; } = null!;
}
