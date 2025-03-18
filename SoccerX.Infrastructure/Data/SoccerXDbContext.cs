using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;

namespace SoccerX.Infrastructure.Data;

public partial class SoccerXDbContext : DbContext
{
    public SoccerXDbContext(DbContextOptions<SoccerXDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLogs> AuditLogs { get; set; }

    public virtual DbSet<BetSlips> BetSlips { get; set; }

    public virtual DbSet<Comments> Comments { get; set; }

    public virtual DbSet<Likes> Likes { get; set; }

    public virtual DbSet<Notifications> Notifications { get; set; }

    public virtual DbSet<Payments> Payments { get; set; }

    public virtual DbSet<ReferralRewards> ReferralRewards { get; set; }

    public virtual DbSet<Subscriptions> Subscriptions { get; set; }

    public virtual DbSet<Teams> Teams { get; set; }

    public virtual DbSet<Transactions> Transactions { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auditaction", new[] { "Create", "Update", "Delete", "Restore" })
            .HasPostgresEnum("betslip_status", new[] { "Pending", "Won", "Lost" })
            .HasPostgresEnum("betslipstatus", new[] { "Pending", "Won", "Lost" })
            .HasPostgresEnum("payment_status", new[] { "Pending", "Completed", "Failed" })
            .HasPostgresEnum("paymentmethod", new[] { "CreditCard", "PayPal", "Crypto" })
            .HasPostgresEnum("paymentstatus", new[] { "Pending", "Completed", "Failed", "Refunded" })
            .HasPostgresEnum("referralstatus", new[] { "Pending", "Paid" })
            .HasPostgresEnum("transactions_type", new[] { "Deposit", "Withdrawal", "Subscription" })
            .HasPostgresEnum("transactiontype", new[] { "Deposit", "Withdrawal", "Subscription", "BetSlipPurchase" })
            .HasPostgresEnum("userrole", new[] { "Admin", "User", "Guest" })
            .HasPostgresEnum("userstatus", new[] { "Active", "Banned" });

        modelBuilder.Entity<AuditLogs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("audit_logs_pkey");

            entity.ToTable("audit_logs");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.EntityName)
                .HasMaxLength(50)
                .HasColumnName("entity_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<BetSlips>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bet_slips_pkey");

            entity.ToTable("bet_slips");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsPremium)
                .HasDefaultValue(false)
                .HasColumnName("is_premium");
            entity.Property(e => e.LikeCount)
                .HasDefaultValue(0)
                .HasColumnName("like_count");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.BetSlips)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("bet_slips_user_id_fkey");
        });

        modelBuilder.Entity<Comments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comments_pkey");

            entity.ToTable("comments");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.BetSlipId).HasColumnName("bet_slip_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.BetSlip).WithMany(p => p.Comments)
                .HasForeignKey(d => d.BetSlipId)
                .HasConstraintName("comments_bet_slip_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("comments_user_id_fkey");
        });

        modelBuilder.Entity<Likes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("likes_pkey");

            entity.ToTable("likes");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.BetSlipId).HasColumnName("bet_slip_id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.BetSlip).WithMany(p => p.Likes)
                .HasForeignKey(d => d.BetSlipId)
                .HasConstraintName("likes_bet_slip_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Likes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("likes_user_id_fkey");
        });

        modelBuilder.Entity<Notifications>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notifications_pkey");

            entity.ToTable("notifications");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("notifications_user_id_fkey");
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
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("payment_date");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("payments_user_id_fkey");
        });

        modelBuilder.Entity<ReferralRewards>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("referral_rewards_pkey");

            entity.ToTable("referral_rewards");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.ReferrerId).HasColumnName("referrer_id");

            entity.HasOne(d => d.Referrer).WithMany(p => p.ReferralRewards)
                .HasForeignKey(d => d.ReferrerId)
                .HasConstraintName("referral_rewards_referrer_id_fkey");
        });

        modelBuilder.Entity<Subscriptions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscriptions_pkey");

            entity.ToTable("subscriptions");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.EditorId).HasColumnName("editor_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_date");
            entity.Property(e => e.SubscriberId).HasColumnName("subscriber_id");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");

            entity.HasOne(d => d.Editor).WithMany(p => p.SubscriptionsEditor)
                .HasForeignKey(d => d.EditorId)
                .HasConstraintName("subscriptions_editor_id_fkey");

            entity.HasOne(d => d.Subscriber).WithMany(p => p.SubscriptionsSubscriber)
                .HasForeignKey(d => d.SubscriberId)
                .HasConstraintName("subscriptions_subscriber_id_fkey");
        });

        modelBuilder.Entity<Teams>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("teams_pkey");

            entity.ToTable("teams");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Tags)
                .HasDefaultValueSql("'{}'::jsonb")
                .HasColumnType("jsonb")
                .HasColumnName("tags");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
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
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ReferenceId).HasColumnName("reference_id");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("transactions_user_id_fkey");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.BanEndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ban_end_date");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.ReferralUserId).HasColumnName("referral_user_id");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_date");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.ReferralUser).WithMany(p => p.InverseReferralUser)
                .HasForeignKey(d => d.ReferralUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_referral_user");

            entity.HasMany(d => d.Blocked).WithMany(p => p.Blocker)
                .UsingEntity<Dictionary<string, object>>(
                    "BlockedUsers",
                    r => r.HasOne<Users>().WithMany()
                        .HasForeignKey("BlockedId")
                        .HasConstraintName("blocked_users_blocked_id_fkey"),
                    l => l.HasOne<Users>().WithMany()
                        .HasForeignKey("BlockerId")
                        .HasConstraintName("blocked_users_blocker_id_fkey"),
                    j =>
                    {
                        j.HasKey("BlockerId", "BlockedId").HasName("blocked_users_pkey");
                        j.ToTable("blocked_users");
                        j.IndexerProperty<Guid>("BlockerId").HasColumnName("blocker_id");
                        j.IndexerProperty<Guid>("BlockedId").HasColumnName("blocked_id");
                    });

            entity.HasMany(d => d.Blocker).WithMany(p => p.Blocked)
                .UsingEntity<Dictionary<string, object>>(
                    "BlockedUsers",
                    r => r.HasOne<Users>().WithMany()
                        .HasForeignKey("BlockerId")
                        .HasConstraintName("blocked_users_blocker_id_fkey"),
                    l => l.HasOne<Users>().WithMany()
                        .HasForeignKey("BlockedId")
                        .HasConstraintName("blocked_users_blocked_id_fkey"),
                    j =>
                    {
                        j.HasKey("BlockerId", "BlockedId").HasName("blocked_users_pkey");
                        j.ToTable("blocked_users");
                        j.IndexerProperty<Guid>("BlockerId").HasColumnName("blocker_id");
                        j.IndexerProperty<Guid>("BlockedId").HasColumnName("blocked_id");
                    });

            entity.HasMany(d => d.Follower).WithMany(p => p.Following)
                .UsingEntity<Dictionary<string, object>>(
                    "Followers",
                    r => r.HasOne<Users>().WithMany()
                        .HasForeignKey("FollowerId")
                        .HasConstraintName("followers_follower_id_fkey"),
                    l => l.HasOne<Users>().WithMany()
                        .HasForeignKey("FollowingId")
                        .HasConstraintName("followers_following_id_fkey"),
                    j =>
                    {
                        j.HasKey("FollowerId", "FollowingId").HasName("followers_pkey");
                        j.ToTable("followers");
                        j.IndexerProperty<Guid>("FollowerId").HasColumnName("follower_id");
                        j.IndexerProperty<Guid>("FollowingId").HasColumnName("following_id");
                    });

            entity.HasMany(d => d.Following).WithMany(p => p.Follower)
                .UsingEntity<Dictionary<string, object>>(
                    "Followers",
                    r => r.HasOne<Users>().WithMany()
                        .HasForeignKey("FollowingId")
                        .HasConstraintName("followers_following_id_fkey"),
                    l => l.HasOne<Users>().WithMany()
                        .HasForeignKey("FollowerId")
                        .HasConstraintName("followers_follower_id_fkey"),
                    j =>
                    {
                        j.HasKey("FollowerId", "FollowingId").HasName("followers_pkey");
                        j.ToTable("followers");
                        j.IndexerProperty<Guid>("FollowerId").HasColumnName("follower_id");
                        j.IndexerProperty<Guid>("FollowingId").HasColumnName("following_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
