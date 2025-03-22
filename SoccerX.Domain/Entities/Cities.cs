using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class Cities
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid Countryid { get; set; }

    public DateTime? Createdate { get; set; }

    public DateTime? Updatedate { get; set; }

    public virtual Countries Country { get; set; } = null!;

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}
