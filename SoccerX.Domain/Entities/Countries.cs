using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class Countries
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Countrycode { get; set; } = null!;

    public DateTime? Createdate { get; set; }

    public DateTime? Updatedate { get; set; }

    public virtual ICollection<Cities> Cities { get; set; } = new List<Cities>();

    public virtual ICollection<Users> Users { get; set; } = new List<Users>();
}
