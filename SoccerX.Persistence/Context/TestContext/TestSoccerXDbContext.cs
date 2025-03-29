using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Context.TestContext;

public partial class TestSoccerXDbContext : DbContext
{
    public TestSoccerXDbContext(DbContextOptions<TestSoccerXDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditlog> Auditlog { get; set; }

    public virtual DbSet<Betslips> Betslips { get; set; }

    public virtual DbSet<Cities> Cities { get; set; }

    public virtual DbSet<Comments> Comments { get; set; }

    public virtual DbSet<Countries> Countries { get; set; }

    public virtual DbSet<Likes> Likes { get; set; }

    public virtual DbSet<Notifications> Notifications { get; set; }

    public virtual DbSet<Payments> Payments { get; set; }

    public virtual DbSet<Referralrewards> Referralrewards { get; set; }

    public virtual DbSet<Schemaversions> Schemaversions { get; set; }

    public virtual DbSet<Subscriptions> Subscriptions { get; set; }

    public virtual DbSet<Teams> Teams { get; set; }

    public virtual DbSet<Transactions> Transactions { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auditaction", new[] { "Create", "Update", "Delete", "Restore" })
            .HasPostgresEnum("betslipstatus", new[] { "Pending", "Won", "Lost" })
            .HasPostgresEnum("paymentmethod", new[] { "CreditCard", "PayPal", "Crypto" })
            .HasPostgresEnum("paymentstatus", new[] { "Pending", "Completed", "Failed", "Refunded" })
            .HasPostgresEnum("referralstatus", new[] { "Pending", "Paid" })
            .HasPostgresEnum("transactiontype", new[] { "Deposit", "Withdrawal", "Subscription", "BetSlipPurchase" })
            .HasPostgresEnum("userrole", new[] { "User", "Editor", "Admin" })
            .HasPostgresEnum("userstatus", new[] { "Active", "Banned" });

        modelBuilder.Entity<Auditlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auditlog_pkey");

            entity.ToTable("auditlog");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Details).HasColumnName("details");
            entity.Property(e => e.Entityid).HasColumnName("entityid");
            entity.Property(e => e.Entityname)
                .HasMaxLength(50)
                .HasColumnName("entityname");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Performedby).HasColumnName("performedby");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestamp");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");

            entity.HasOne(d => d.PerformedbyNavigation).WithMany(p => p.Auditlog)
                .HasForeignKey(d => d.Performedby)
                .HasConstraintName("auditlog_performedby_fkey");
        });

        modelBuilder.Entity<Betslips>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("betslips_pkey");

            entity.ToTable("betslips");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Commentcount)
                .HasDefaultValue(0)
                .HasColumnName("commentcount");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Ispremium)
                .HasDefaultValue(false)
                .HasColumnName("ispremium");
            entity.Property(e => e.Likecount)
                .HasDefaultValue(0)
                .HasColumnName("likecount");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Betslips)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("betslips_userid_fkey");
        });

        modelBuilder.Entity<Cities>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cities_pkey");

            entity.ToTable("cities");

            entity.HasIndex(e => new { e.Name, e.Countryid }, "unique_city_country").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.Countryid)
                .HasConstraintName("cities_countryid_fkey");
        });

        modelBuilder.Entity<Comments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comments_pkey");

            entity.ToTable("comments");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Betslipid).HasColumnName("betslipid");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Betslip).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Betslipid)
                .HasConstraintName("comments_betslipid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("comments_userid_fkey");
        });

        modelBuilder.Entity<Countries>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("countries_pkey");

            entity.ToTable("countries");

            entity.HasIndex(e => e.Countrycode, "countries_countrycode_key").IsUnique();

            entity.HasIndex(e => e.Name, "countries_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Countrycode)
                .HasMaxLength(10)
                .HasColumnName("countrycode");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");
        });

        modelBuilder.Entity<Likes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("likes_pkey");

            entity.ToTable("likes");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Betslipid).HasColumnName("betslipid");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Betslip).WithMany(p => p.Likes)
                .HasForeignKey(d => d.Betslipid)
                .HasConstraintName("likes_betslipid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Likes)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("likes_userid_fkey");
        });

        modelBuilder.Entity<Notifications>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notifications_pkey");

            entity.ToTable("notifications");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Isread)
                .HasDefaultValue(false)
                .HasColumnName("isread");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("notifications_userid_fkey");
        });

        modelBuilder.Entity<Payments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Paymentdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("paymentdate");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("payments_userid_fkey");
        });

        modelBuilder.Entity<Referralrewards>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("referralrewards_pkey");

            entity.ToTable("referralrewards");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Referrerid).HasColumnName("referrerid");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Referrer).WithMany(p => p.ReferralrewardsReferrer)
                .HasForeignKey(d => d.Referrerid)
                .HasConstraintName("referralrewards_referrerid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ReferralrewardsUser)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("referralrewards_userid_fkey");
        });

        modelBuilder.Entity<Schemaversions>(entity =>
        {
            entity.HasKey(e => e.Schemaversionsid).HasName("PK_schemaversions_Id");

            entity.ToTable("schemaversions");

            entity.Property(e => e.Schemaversionsid).HasColumnName("schemaversionsid");
            entity.Property(e => e.Applied)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("applied");
            entity.Property(e => e.Scriptname)
                .HasMaxLength(255)
                .HasColumnName("scriptname");
        });

        modelBuilder.Entity<Subscriptions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscriptions_pkey");

            entity.ToTable("subscriptions");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Editorid).HasColumnName("editorid");
            entity.Property(e => e.Enddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enddate");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Startdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startdate");
            entity.Property(e => e.Subscriberid).HasColumnName("subscriberid");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");

            entity.HasOne(d => d.Editor).WithMany(p => p.SubscriptionsEditor)
                .HasForeignKey(d => d.Editorid)
                .HasConstraintName("subscriptions_editorid_fkey");

            entity.HasOne(d => d.Subscriber).WithMany(p => p.SubscriptionsSubscriber)
                .HasForeignKey(d => d.Subscriberid)
                .HasConstraintName("subscriptions_subscriberid_fkey");
        });

        modelBuilder.Entity<Teams>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("teams_pkey");

            entity.ToTable("teams");

            entity.HasIndex(e => e.Name, "teams_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Tags)
                .HasColumnType("jsonb")
                .HasColumnName("tags");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");
        });

        modelBuilder.Entity<Transactions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transactions_pkey");

            entity.ToTable("transactions");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Referenceid).HasColumnName("referenceid");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("transactions_userid_fkey");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Phonenumber, "users_phonenumber_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Avatarurl).HasColumnName("avatarurl");
            entity.Property(e => e.Banenddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("banenddate");
            entity.Property(e => e.Cityid).HasColumnName("cityid");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Followercount)
                .HasDefaultValue(0)
                .HasColumnName("followercount");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Passwordhash).HasColumnName("passwordhash");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Postalcode)
                .HasMaxLength(20)
                .HasColumnName("postalcode");
            entity.Property(e => e.Referraluserid).HasColumnName("referraluserid");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.Cityid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("users_cityid_fkey");

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .HasForeignKey(d => d.Countryid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("users_countryid_fkey");

            entity.HasOne(d => d.Referraluser).WithMany(p => p.InverseReferraluser)
                .HasForeignKey(d => d.Referraluserid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_referraluserid_fkey");

            entity.HasMany(d => d.Blocked).WithMany(p => p.Blocker)
                .UsingEntity<Dictionary<string, object>>(
                    "Blockedusers",
                    r => r.HasOne<Users>().WithMany()
                        .HasForeignKey("Blockedid")
                        .HasConstraintName("blockedusers_blockedid_fkey"),
                    l => l.HasOne<Users>().WithMany()
                        .HasForeignKey("Blockerid")
                        .HasConstraintName("blockedusers_blockerid_fkey"),
                    j =>
                    {
                        j.HasKey("Blockerid", "Blockedid").HasName("blockedusers_pkey");
                        j.ToTable("blockedusers");
                        j.IndexerProperty<Guid>("Blockerid").HasColumnName("blockerid");
                        j.IndexerProperty<Guid>("Blockedid").HasColumnName("blockedid");
                    });

            entity.HasMany(d => d.Blocker).WithMany(p => p.Blocked)
                .UsingEntity<Dictionary<string, object>>(
                    "Blockedusers",
                    r => r.HasOne<Users>().WithMany()
                        .HasForeignKey("Blockerid")
                        .HasConstraintName("blockedusers_blockerid_fkey"),
                    l => l.HasOne<Users>().WithMany()
                        .HasForeignKey("Blockedid")
                        .HasConstraintName("blockedusers_blockedid_fkey"),
                    j =>
                    {
                        j.HasKey("Blockerid", "Blockedid").HasName("blockedusers_pkey");
                        j.ToTable("blockedusers");
                        j.IndexerProperty<Guid>("Blockerid").HasColumnName("blockerid");
                        j.IndexerProperty<Guid>("Blockedid").HasColumnName("blockedid");
                    });

            entity.HasMany(d => d.Follower).WithMany(p => p.Following)
                .UsingEntity<Dictionary<string, object>>(
                    "Followers",
                    r => r.HasOne<Users>().WithMany()
                        .HasForeignKey("Followerid")
                        .HasConstraintName("followers_followerid_fkey"),
                    l => l.HasOne<Users>().WithMany()
                        .HasForeignKey("Followingid")
                        .HasConstraintName("followers_followingid_fkey"),
                    j =>
                    {
                        j.HasKey("Followerid", "Followingid").HasName("followers_pkey");
                        j.ToTable("followers");
                        j.IndexerProperty<Guid>("Followerid").HasColumnName("followerid");
                        j.IndexerProperty<Guid>("Followingid").HasColumnName("followingid");
                    });

            entity.HasMany(d => d.Following).WithMany(p => p.Follower)
                .UsingEntity<Dictionary<string, object>>(
                    "Followers",
                    r => r.HasOne<Users>().WithMany()
                        .HasForeignKey("Followingid")
                        .HasConstraintName("followers_followingid_fkey"),
                    l => l.HasOne<Users>().WithMany()
                        .HasForeignKey("Followerid")
                        .HasConstraintName("followers_followerid_fkey"),
                    j =>
                    {
                        j.HasKey("Followerid", "Followingid").HasName("followers_pkey");
                        j.ToTable("followers");
                        j.IndexerProperty<Guid>("Followerid").HasColumnName("followerid");
                        j.IndexerProperty<Guid>("Followingid").HasColumnName("followingid");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
