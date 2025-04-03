using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class Emailverifications
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public DateTime Expiresat { get; set; }

    public bool? Isused { get; set; }

    public DateTime? Createdate { get; set; }

    public string Code { get; set; } = null!;

    public virtual Users User { get; set; } = null!;
}
