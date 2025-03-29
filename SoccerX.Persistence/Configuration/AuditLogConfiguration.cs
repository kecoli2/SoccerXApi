using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Configuration
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<Auditlog>
    {
        public void Configure(EntityTypeBuilder<Auditlog> entity)
        {
            entity.ToTable("auditlog");

            entity.HasKey(e => e.Id).HasName("auditlog_pkey");
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("id");
            entity.Property(e => e.Entityname).HasMaxLength(50).HasColumnName("entityname").IsRequired();
            entity.Property(e => e.Entityid).HasColumnName("entityid").IsRequired();
            entity.Property(e => e.Details).HasColumnName("details");
            entity.Property(e => e.Performedby).HasColumnName("performedby").IsRequired();
            entity.Property(e => e.Createdate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("createdate");            
            entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("timestamp");
            entity.Property(e => e.Isdeleted).HasDefaultValue(false).HasColumnName("isdeleted");
            //MANUEL ADDS
            entity.Property(e => e.Action).HasColumnType("auditaction").HasColumnName("action").IsRequired();

            entity.HasOne(d => d.PerformedbyNavigation).WithMany(p => p.Auditlog)
                .HasForeignKey(d => d.Performedby)
                .HasConstraintName("auditlog_performedby_fkey");
        }
    }
}
