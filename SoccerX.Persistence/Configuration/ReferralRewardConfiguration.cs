using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Configuration
{
    public class ReferralRewardConfiguration : IEntityTypeConfiguration<Referralrewards>
    {
        public void Configure(EntityTypeBuilder<Referralrewards> entity)
        {
            entity.ToTable("referralrewards");

            entity.HasKey(e => e.Id).HasName("referralrewards_pkey");
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("id");
            entity.Property(e => e.Amount).HasPrecision(10, 2).HasColumnName("amount");
            entity.Property(e => e.Referrerid).HasColumnName("referrerid");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Createdate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("createdate");
            entity.Property(e => e.Updatedate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("updatedate");
            entity.Property(e => e.Isdeleted).HasDefaultValue(false).HasColumnName("isdeleted");
            entity.Property(e => e.Status).HasColumnName("status").HasColumnType("referralstatus");

            entity.HasOne(e => e.User).WithMany(u => u.ReferralrewardsUser).HasForeignKey(e => e.Userid).HasConstraintName("referralrewards_userid_fkey");
            entity.HasOne(e => e.Referrer).WithMany(u => u.ReferralrewardsReferrer).HasForeignKey(e => e.Referrerid).HasConstraintName("referralrewards_referrerid_fkey");
        }
    }
}
