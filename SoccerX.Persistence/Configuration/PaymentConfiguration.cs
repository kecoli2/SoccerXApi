using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payments>
    {
        public void Configure(EntityTypeBuilder<Payments> entity)
        {
            entity.ToTable("payments");

            entity.HasKey(e => e.Id).HasName("payments_pkey");
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("id");
            entity.Property(e => e.Amount).HasPrecision(10, 2).HasColumnName("amount");
            entity.Property(e => e.Paymentdate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("paymentdate");
            entity.Property(e => e.Createdate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("createdate");
            entity.Property(e => e.Updatedate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("updatedate");
            entity.Property(e => e.Isdeleted).HasDefaultValue(false).HasColumnName("isdeleted");
            entity.Property(e => e.PaymentStatus).HasColumnName("paymentstatus").HasColumnType("paymentstatus");
            entity.Property(e => e.PaymentMethod).HasColumnName("paymentmethod").HasColumnType("paymentmethod");

            entity.HasOne(e => e.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(e => e.Userid)
                .HasConstraintName("payments_userid_fkey");
        }
    }
}
