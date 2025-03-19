using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class Betslips
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public bool Ispremium { get; set; }

    public int Likecount { get; set; }

    public DateTime? Createdate { get; set; }

    public DateTime? Updatedate { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<Comments> Comments { get; set; } = new List<Comments>();

    public virtual ICollection<Likes> Likes { get; set; } = new List<Likes>();

    public virtual Users User { get; set; } = null!;
}
