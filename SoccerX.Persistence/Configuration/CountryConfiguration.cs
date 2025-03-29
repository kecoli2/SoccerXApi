using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Configuration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Countries>
    {
        public void Configure(EntityTypeBuilder<Countries> entity)
        {
            entity.ToTable("countries");

            entity.HasKey(e => e.Id).HasName("countries_pkey");
            entity.HasIndex(e => e.Name).IsUnique().HasDatabaseName("countries_name_key");
            entity.HasIndex(e => e.Countrycode).IsUnique().HasDatabaseName("countries_countrycode_key");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("name");
            entity.Property(e => e.Countrycode).HasMaxLength(10).HasColumnName("countrycode");
            entity.Property(e => e.Createdate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("createdate");
            entity.Property(e => e.Updatedate).HasColumnType("timestamp without time zone").HasColumnName("updatedate");
        }
    }
}
