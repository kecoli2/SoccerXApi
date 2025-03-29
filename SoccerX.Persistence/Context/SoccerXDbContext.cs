using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Configuration;

namespace SoccerX.Persistence.Context;

public partial class SoccerXDbContext : DbContext
{
    public SoccerXDbContext(DbContextOptions<SoccerXDbContext> options)
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

    public virtual DbSet<Subscriptions> Subscriptions { get; set; }

    public virtual DbSet<Teams> Teams { get; set; }

    public virtual DbSet<Transactions> Transactions { get; set; }

    public virtual DbSet<Users> Users { get; set; }
    public virtual DbSet<Emailverifications> Emailverifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TeamConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new LikeConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
        modelBuilder.ApplyConfiguration(new ReferralRewardConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
        modelBuilder.ApplyConfiguration(new BetSlipConfiguration());
        modelBuilder.ApplyConfiguration(new EmailVerificationConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
