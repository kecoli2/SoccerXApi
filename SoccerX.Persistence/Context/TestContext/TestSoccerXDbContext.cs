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

    public virtual DbSet<Emailverifications> Emailverifications { get; set; }

    public virtual DbSet<Likes> Likes { get; set; }

    public virtual DbSet<Notifications> Notifications { get; set; }

    public virtual DbSet<Payments> Payments { get; set; }

    public virtual DbSet<QrtzBlobTriggers> QrtzBlobTriggers { get; set; }

    public virtual DbSet<QrtzCalendars> QrtzCalendars { get; set; }

    public virtual DbSet<QrtzCronTriggers> QrtzCronTriggers { get; set; }

    public virtual DbSet<QrtzFiredTriggers> QrtzFiredTriggers { get; set; }

    public virtual DbSet<QrtzJobDetails> QrtzJobDetails { get; set; }

    public virtual DbSet<QrtzLocks> QrtzLocks { get; set; }

    public virtual DbSet<QrtzPausedTriggerGrps> QrtzPausedTriggerGrps { get; set; }

    public virtual DbSet<QrtzSchedulerState> QrtzSchedulerState { get; set; }

    public virtual DbSet<QrtzSimpleTriggers> QrtzSimpleTriggers { get; set; }

    public virtual DbSet<QrtzSimpropTriggers> QrtzSimpropTriggers { get; set; }

    public virtual DbSet<QrtzTriggers> QrtzTriggers { get; set; }

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

        modelBuilder.Entity<Emailverifications>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("emailverifications_pkey");

            entity.ToTable("emailverifications");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Expiresat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiresat");
            entity.Property(e => e.Isused)
                .HasDefaultValue(false)
                .HasColumnName("isused");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .HasColumnName("token");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Emailverifications)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("fk_user_emailverification");
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

        modelBuilder.Entity<QrtzBlobTriggers>(entity =>
        {
            entity.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup }).HasName("qrtz_blob_triggers_pkey");

            entity.ToTable("qrtz_blob_triggers");

            entity.Property(e => e.SchedName).HasColumnName("sched_name");
            entity.Property(e => e.TriggerName).HasColumnName("trigger_name");
            entity.Property(e => e.TriggerGroup).HasColumnName("trigger_group");
            entity.Property(e => e.BlobData).HasColumnName("blob_data");

            entity.HasOne(d => d.QrtzTriggers).WithOne(p => p.QrtzBlobTriggers)
                .HasForeignKey<QrtzBlobTriggers>(d => new { d.SchedName, d.TriggerName, d.TriggerGroup })
                .HasConstraintName("qrtz_blob_triggers_sched_name_trigger_name_trigger_group_fkey");
        });

        modelBuilder.Entity<QrtzCalendars>(entity =>
        {
            entity.HasKey(e => new { e.SchedName, e.CalendarName }).HasName("qrtz_calendars_pkey");

            entity.ToTable("qrtz_calendars");

            entity.Property(e => e.SchedName).HasColumnName("sched_name");
            entity.Property(e => e.CalendarName).HasColumnName("calendar_name");
            entity.Property(e => e.Calendar).HasColumnName("calendar");
        });

        modelBuilder.Entity<QrtzCronTriggers>(entity =>
        {
            entity.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup }).HasName("qrtz_cron_triggers_pkey");

            entity.ToTable("qrtz_cron_triggers");

            entity.Property(e => e.SchedName).HasColumnName("sched_name");
            entity.Property(e => e.TriggerName).HasColumnName("trigger_name");
            entity.Property(e => e.TriggerGroup).HasColumnName("trigger_group");
            entity.Property(e => e.CronExpression).HasColumnName("cron_expression");
            entity.Property(e => e.TimeZoneId).HasColumnName("time_zone_id");

            entity.HasOne(d => d.QrtzTriggers).WithOne(p => p.QrtzCronTriggers)
                .HasForeignKey<QrtzCronTriggers>(d => new { d.SchedName, d.TriggerName, d.TriggerGroup })
                .HasConstraintName("qrtz_cron_triggers_sched_name_trigger_name_trigger_group_fkey");
        });

        modelBuilder.Entity<QrtzFiredTriggers>(entity =>
        {
            entity.HasKey(e => new { e.SchedName, e.EntryId }).HasName("qrtz_fired_triggers_pkey");

            entity.ToTable("qrtz_fired_triggers");

            entity.HasIndex(e => e.JobGroup, "idx_qrtz_ft_job_group");

            entity.HasIndex(e => e.JobName, "idx_qrtz_ft_job_name");

            entity.HasIndex(e => e.RequestsRecovery, "idx_qrtz_ft_job_req_recovery");

            entity.HasIndex(e => e.TriggerGroup, "idx_qrtz_ft_trig_group");

            entity.HasIndex(e => e.InstanceName, "idx_qrtz_ft_trig_inst_name");

            entity.HasIndex(e => e.TriggerName, "idx_qrtz_ft_trig_name");

            entity.HasIndex(e => new { e.SchedName, e.TriggerName, e.TriggerGroup }, "idx_qrtz_ft_trig_nm_gp");

            entity.Property(e => e.SchedName).HasColumnName("sched_name");
            entity.Property(e => e.EntryId).HasColumnName("entry_id");
            entity.Property(e => e.FiredTime).HasColumnName("fired_time");
            entity.Property(e => e.InstanceName).HasColumnName("instance_name");
            entity.Property(e => e.IsNonconcurrent).HasColumnName("is_nonconcurrent");
            entity.Property(e => e.JobGroup).HasColumnName("job_group");
            entity.Property(e => e.JobName).HasColumnName("job_name");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.RequestsRecovery).HasColumnName("requests_recovery");
            entity.Property(e => e.SchedTime).HasColumnName("sched_time");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.TriggerGroup).HasColumnName("trigger_group");
            entity.Property(e => e.TriggerName).HasColumnName("trigger_name");
        });

        modelBuilder.Entity<QrtzJobDetails>(entity =>
        {
            entity.HasKey(e => new { e.SchedName, e.JobName, e.JobGroup }).HasName("qrtz_job_details_pkey");

            entity.ToTable("qrtz_job_details");

            entity.HasIndex(e => e.RequestsRecovery, "idx_qrtz_j_req_recovery");

            entity.Property(e => e.SchedName).HasColumnName("sched_name");
            entity.Property(e => e.JobName).HasColumnName("job_name");
            entity.Property(e => e.JobGroup).HasColumnName("job_group");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsDurable).HasColumnName("is_durable");
            entity.Property(e => e.IsNonconcurrent).HasColumnName("is_nonconcurrent");
            entity.Property(e => e.IsUpdateData).HasColumnName("is_update_data");
            entity.Property(e => e.JobClassName).HasColumnName("job_class_name");
            entity.Property(e => e.JobData).HasColumnName("job_data");
            entity.Property(e => e.RequestsRecovery).HasColumnName("requests_recovery");
        });

        modelBuilder.Entity<QrtzLocks>(entity =>
        {
            entity.HasKey(e => new { e.SchedName, e.LockName }).HasName("qrtz_locks_pkey");

            entity.ToTable("qrtz_locks");

            entity.Property(e => e.SchedName).HasColumnName("sched_name");
            entity.Property(e => e.LockName).HasColumnName("lock_name");
        });

        modelBuilder.Entity<QrtzPausedTriggerGrps>(entity =>
        {
            entity.HasKey(e => new { e.SchedName, e.TriggerGroup }).HasName("qrtz_paused_trigger_grps_pkey");

            entity.ToTable("qrtz_paused_trigger_grps");

            entity.Property(e => e.SchedName).HasColumnName("sched_name");
            entity.Property(e => e.TriggerGroup).HasColumnName("trigger_group");
        });

        modelBuilder.Entity<QrtzSchedulerState>(entity =>
        {
            entity.HasKey(e => new { e.SchedName, e.InstanceName }).HasName("qrtz_scheduler_state_pkey");

            entity.ToTable("qrtz_scheduler_state");

            entity.Property(e => e.SchedName).HasColumnName("sched_name");
            entity.Property(e => e.InstanceName).HasColumnName("instance_name");
            entity.Property(e => e.CheckinInterval).HasColumnName("checkin_interval");
            entity.Property(e => e.LastCheckinTime).HasColumnName("last_checkin_time");
        });

        modelBuilder.Entity<QrtzSimpleTriggers>(entity =>
        {
            entity.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup }).HasName("qrtz_simple_triggers_pkey");

            entity.ToTable("qrtz_simple_triggers");

            entity.Property(e => e.SchedName).HasColumnName("sched_name");
            entity.Property(e => e.TriggerName).HasColumnName("trigger_name");
            entity.Property(e => e.TriggerGroup).HasColumnName("trigger_group");
            entity.Property(e => e.RepeatCount).HasColumnName("repeat_count");
            entity.Property(e => e.RepeatInterval).HasColumnName("repeat_interval");
            entity.Property(e => e.TimesTriggered).HasColumnName("times_triggered");

            entity.HasOne(d => d.QrtzTriggers).WithOne(p => p.QrtzSimpleTriggers)
                .HasForeignKey<QrtzSimpleTriggers>(d => new { d.SchedName, d.TriggerName, d.TriggerGroup })
                .HasConstraintName("qrtz_simple_triggers_sched_name_trigger_name_trigger_group_fkey");
        });

        modelBuilder.Entity<QrtzSimpropTriggers>(entity =>
        {
            entity.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup }).HasName("qrtz_simprop_triggers_pkey");

            entity.ToTable("qrtz_simprop_triggers");

            entity.Property(e => e.SchedName).HasColumnName("sched_name");
            entity.Property(e => e.TriggerName).HasColumnName("trigger_name");
            entity.Property(e => e.TriggerGroup).HasColumnName("trigger_group");
            entity.Property(e => e.BoolProp1).HasColumnName("bool_prop_1");
            entity.Property(e => e.BoolProp2).HasColumnName("bool_prop_2");
            entity.Property(e => e.DecProp1).HasColumnName("dec_prop_1");
            entity.Property(e => e.DecProp2).HasColumnName("dec_prop_2");
            entity.Property(e => e.IntProp1).HasColumnName("int_prop_1");
            entity.Property(e => e.IntProp2).HasColumnName("int_prop_2");
            entity.Property(e => e.LongProp1).HasColumnName("long_prop_1");
            entity.Property(e => e.LongProp2).HasColumnName("long_prop_2");
            entity.Property(e => e.StrProp1).HasColumnName("str_prop_1");
            entity.Property(e => e.StrProp2).HasColumnName("str_prop_2");
            entity.Property(e => e.StrProp3).HasColumnName("str_prop_3");
            entity.Property(e => e.TimeZoneId).HasColumnName("time_zone_id");

            entity.HasOne(d => d.QrtzTriggers).WithOne(p => p.QrtzSimpropTriggers)
                .HasForeignKey<QrtzSimpropTriggers>(d => new { d.SchedName, d.TriggerName, d.TriggerGroup })
                .HasConstraintName("qrtz_simprop_triggers_sched_name_trigger_name_trigger_grou_fkey");
        });

        modelBuilder.Entity<QrtzTriggers>(entity =>
        {
            entity.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup }).HasName("qrtz_triggers_pkey");

            entity.ToTable("qrtz_triggers");

            entity.HasIndex(e => e.NextFireTime, "idx_qrtz_t_next_fire_time");

            entity.HasIndex(e => new { e.NextFireTime, e.TriggerState }, "idx_qrtz_t_nft_st");

            entity.HasIndex(e => e.TriggerState, "idx_qrtz_t_state");

            entity.Property(e => e.SchedName).HasColumnName("sched_name");
            entity.Property(e => e.TriggerName).HasColumnName("trigger_name");
            entity.Property(e => e.TriggerGroup).HasColumnName("trigger_group");
            entity.Property(e => e.CalendarName).HasColumnName("calendar_name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.JobData).HasColumnName("job_data");
            entity.Property(e => e.JobGroup).HasColumnName("job_group");
            entity.Property(e => e.JobName).HasColumnName("job_name");
            entity.Property(e => e.MisfireInstr).HasColumnName("misfire_instr");
            entity.Property(e => e.NextFireTime).HasColumnName("next_fire_time");
            entity.Property(e => e.PrevFireTime).HasColumnName("prev_fire_time");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.TriggerState).HasColumnName("trigger_state");
            entity.Property(e => e.TriggerType).HasColumnName("trigger_type");

            entity.HasOne(d => d.QrtzJobDetails).WithMany(p => p.QrtzTriggers)
                .HasForeignKey(d => new { d.SchedName, d.JobName, d.JobGroup })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("qrtz_triggers_sched_name_job_name_job_group_fkey");
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
            entity.Property(e => e.Isemailconfirmed)
                .HasDefaultValue(false)
                .HasColumnName("isemailconfirmed");
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
