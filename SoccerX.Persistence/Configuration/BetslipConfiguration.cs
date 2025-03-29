using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Persistence.Configuration
{
    public class BetSlipConfiguration : IEntityTypeConfiguration<Betslips>
    {
        public void Configure(EntityTypeBuilder<Betslips> entity)
        {
            entity.ToTable("betslips");

            entity.HasKey(e => e.Id).HasName("betslips_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("id");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Likecount).HasDefaultValue(0).HasColumnName("likecount");
            entity.Property(e => e.Commentcount).HasDefaultValue(0).HasColumnName("commentcount");
            entity.Property(e => e.Ispremium).HasDefaultValue(false).HasColumnName("ispremium");
            entity.Property(e => e.Isdeleted).HasDefaultValue(false).HasColumnName("isdeleted");
            entity.Property(e => e.Createdate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("createdate");
            entity.Property(e => e.Updatedate).HasColumnType("timestamp without time zone").HasColumnName("updatedate");
            //MANUEL ADDS
            entity.Property(e => e.Status).HasColumnType("betslipstatus").HasColumnName("status").IsRequired();

            entity.HasOne(d => d.User)
                .WithMany(p => p.Betslips)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("betslips_userid_fkey");
        }
    }
}
