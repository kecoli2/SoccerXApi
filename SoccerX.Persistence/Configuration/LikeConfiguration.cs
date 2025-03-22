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
    public class LikeConfiguration : IEntityTypeConfiguration<Likes>
    {
        public void Configure(EntityTypeBuilder<Likes> entity)
        {
            entity.ToTable("likes");

            entity.HasKey(e => e.Id).HasName("likes_pkey");
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("id");
            entity.Property(e => e.Betslipid).HasColumnName("betslipid");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Createdate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("createdate");
            entity.Property(e => e.Updatedate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("updatedate");
            entity.Property(e => e.Isdeleted).HasDefaultValue(false).HasColumnName("isdeleted");

            entity.HasOne(e => e.User).WithMany(u => u.Likes).HasForeignKey(e => e.Userid).HasConstraintName("likes_userid_fkey");
            entity.HasOne(e => e.Betslip).WithMany(b => b.Likes).HasForeignKey(e => e.Betslipid).HasConstraintName("likes_betslipid_fkey");
        }
    }
}
