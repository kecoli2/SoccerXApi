using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> entity)
        {
            entity.ToTable("users");

            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.HasIndex(e => e.Email).IsUnique().HasDatabaseName("users_email_key");
            entity.HasIndex(e => e.Phonenumber).IsUnique().HasDatabaseName("users_phonenumber_key");
            entity.HasIndex(e => e.Username).IsUnique().HasDatabaseName("users_username_key");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("id");
            entity.Property(e => e.Status).HasColumnType("userstatus").HasColumnName("status").IsRequired();
            entity.Property(e => e.Role).HasColumnType("userrole").HasColumnName("role").IsRequired();
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Avatarurl).HasColumnName("avatarurl");
            entity.Property(e => e.Banenddate).HasColumnType("timestamp without time zone").HasColumnName("banenddate");
            entity.Property(e => e.Cityid).HasColumnName("cityid");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Createdate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("createdate");
            entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email");
            entity.Property(e => e.Followercount).HasDefaultValue(0).HasColumnName("followercount");
            entity.Property(e => e.Isdeleted).HasDefaultValue(false).HasColumnName("isdeleted");
            entity.Property(e => e.Passwordhash).HasColumnName("passwordhash");
            entity.Property(e => e.Phonenumber).HasMaxLength(20).HasColumnName("phonenumber");
            entity.Property(e => e.Postalcode).HasMaxLength(20).HasColumnName("postalcode");
            entity.Property(e => e.Referraluserid).HasColumnName("referraluserid");
            entity.Property(e => e.Updatedate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("updatedate");
            entity.Property(e => e.Username).HasMaxLength(50).HasColumnName("username");

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
                    r => r.HasOne<Users>().WithMany().HasForeignKey("Blockedid").HasConstraintName("blockedusers_blockedid_fkey"),
                    l => l.HasOne<Users>().WithMany().HasForeignKey("Blockerid").HasConstraintName("blockedusers_blockerid_fkey"),
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
                    r => r.HasOne<Users>().WithMany().HasForeignKey("Followerid").HasConstraintName("followers_followerid_fkey"),
                    l => l.HasOne<Users>().WithMany().HasForeignKey("Followingid").HasConstraintName("followers_followingid_fkey"),
                    j =>
                    {
                        j.HasKey("Followerid", "Followingid").HasName("followers_pkey");
                        j.ToTable("followers");
                        j.IndexerProperty<Guid>("Followerid").HasColumnName("followerid");
                        j.IndexerProperty<Guid>("Followingid").HasColumnName("followingid");
                    });
        }
    }


}
