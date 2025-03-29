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
    public class CommentConfiguration : IEntityTypeConfiguration<Comments>
    {
        public void Configure(EntityTypeBuilder<Comments> entity)
        {
            entity.ToTable("comments");

            entity.HasKey(e => e.Id).HasName("comments_pkey");
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Betslipid).HasColumnName("betslipid");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Createdate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("createdate");            
            entity.Property(e => e.Isdeleted).HasDefaultValue(false).HasColumnName("isdeleted");

            entity.HasOne(e => e.User).WithMany(u => u.Comments).HasForeignKey(e => e.Userid).HasConstraintName("comments_userid_fkey");
            entity.HasOne(e => e.Betslip).WithMany(b => b.Comments).HasForeignKey(e => e.Betslipid).HasConstraintName("comments_betslipid_fkey");
        }
    }

}
