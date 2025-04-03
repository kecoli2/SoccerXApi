using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Configuration;

public class EmailVerificationConfiguration : IEntityTypeConfiguration<Emailverifications>
{
    public void Configure(EntityTypeBuilder<Emailverifications> builder)
    {
        builder.ToTable("emailverifications");

        builder.HasKey(e => e.Id).HasName("emailverifications_pkey");

        builder.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("id");
        builder.Property(e => e.Userid).IsRequired().HasColumnName("userid");
        builder.Property(e => e.Code).IsRequired().HasMaxLength(6).HasColumnName("code");
        builder.Property(e => e.Expiresat).IsRequired().HasColumnType("timestamp without time zone").HasColumnName("expiresat");
        builder.Property(e => e.Isused).HasDefaultValue(false).HasColumnName("isused");
        builder.Property(e => e.Createdate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("createdate");

        builder.HasOne(e => e.User)
               .WithMany(u => u.Emailverifications)
               .HasForeignKey(e => e.Userid)
               .OnDelete(DeleteBehavior.Cascade)
               .HasConstraintName("fk_user_emailverification");
    }
}
