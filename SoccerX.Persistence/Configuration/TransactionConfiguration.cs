using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transactions>
    {
        public void Configure(EntityTypeBuilder<Transactions> entity)
        {
            entity.ToTable("transactions");

            entity.HasKey(e => e.Id).HasName("transactions_pkey");
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("id");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Amount).HasPrecision(10, 2).HasColumnName("amount");
            entity.Property(e => e.Referenceid).HasColumnName("referenceid");
            entity.Property(e => e.Createdate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("createdate");            
            entity.Property(e => e.Isdeleted).HasDefaultValue(false).HasColumnName("isdeleted");
            entity.Property(e => e.TransactionType).HasColumnType("transactiontype").HasColumnName("transactiontype").IsRequired();
            entity.Property(e => e.Updatedate).HasColumnType("timestamp without time zone").HasColumnName("updatedate");

            entity.HasOne(e => e.User).WithMany(u => u.Transactions).HasForeignKey(e => e.Userid).HasConstraintName("transactions_userid_fkey");
        }
    }
}
