using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class Emailverifications
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public string Token { get; set; } = null!;

    public DateTime Expiresat { get; set; }

    public bool? Isused { get; set; }

    public DateTime? Createdate { get; set; }

    public virtual Users User { get; set; } = null!;
}
