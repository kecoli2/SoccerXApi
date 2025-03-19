using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class Payments
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public decimal Amount { get; set; }

    public DateTime? Paymentdate { get; set; }

    public DateTime? Createdate { get; set; }

    public DateTime? Updatedate { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual Users User { get; set; } = null!;
}
