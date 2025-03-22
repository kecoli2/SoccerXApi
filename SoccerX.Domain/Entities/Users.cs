using System;
using System.Collections.Generic;

namespace SoccerX.Domain.Entities;

public partial class Users
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public DateTime? Banenddate { get; set; }

    public Guid? Referraluserid { get; set; }

    public int Followercount { get; set; }

    public DateTime? Createdate { get; set; }

    public DateTime? Updatedate { get; set; }

    public bool? Isdeleted { get; set; }

    public Guid Countryid { get; set; }

    public Guid Cityid { get; set; }

    public string? Postalcode { get; set; }

    public string Address { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public string? Avatarurl { get; set; }

    public virtual ICollection<Auditlog> Auditlog { get; set; } = new List<Auditlog>();

    public virtual ICollection<Betslips> Betslips { get; set; } = new List<Betslips>();

    public virtual Cities City { get; set; } = null!;

    public virtual ICollection<Comments> Comments { get; set; } = new List<Comments>();

    public virtual Countries Country { get; set; } = null!;

    public virtual ICollection<Users> InverseReferraluser { get; set; } = new List<Users>();

    public virtual ICollection<Likes> Likes { get; set; } = new List<Likes>();

    public virtual ICollection<Notifications> Notifications { get; set; } = new List<Notifications>();

    public virtual ICollection<Payments> Payments { get; set; } = new List<Payments>();

    public virtual ICollection<Referralrewards> ReferralrewardsReferrer { get; set; } = new List<Referralrewards>();

    public virtual ICollection<Referralrewards> ReferralrewardsUser { get; set; } = new List<Referralrewards>();

    public virtual Users? Referraluser { get; set; }

    public virtual ICollection<Subscriptions> SubscriptionsEditor { get; set; } = new List<Subscriptions>();

    public virtual ICollection<Subscriptions> SubscriptionsSubscriber { get; set; } = new List<Subscriptions>();

    public virtual ICollection<Transactions> Transactions { get; set; } = new List<Transactions>();

    public virtual ICollection<Users> Blocked { get; set; } = new List<Users>();

    public virtual ICollection<Users> Blocker { get; set; } = new List<Users>();

    public virtual ICollection<Users> Follower { get; set; } = new List<Users>();

    public virtual ICollection<Users> Following { get; set; } = new List<Users>();
}
