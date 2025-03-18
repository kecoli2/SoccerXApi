using System;
using System.Collections.Generic;

namespace SoccerX.Infrastructure;

public partial class Teams
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string? Tags { get; set; }

    public DateTime? Createdate { get; set; }

    public DateTime? Updatedate { get; set; }

    public bool? Isdeleted { get; set; }
}
