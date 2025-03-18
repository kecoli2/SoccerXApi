using System;
using System.Collections.Generic;

namespace SoccerX.Infrastructure;

public partial class Transactions
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public decimal Amount { get; set; }

    public Guid? Referenceid { get; set; }

    public DateTime? Createdate { get; set; }

    public DateTime? Updatedate { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual Users User { get; set; } = null!;
}
