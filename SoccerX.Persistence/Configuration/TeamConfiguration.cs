using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Teams>
    {
        public void Configure(EntityTypeBuilder<Teams> entity)
        {
            entity.ToTable("teams");

            entity.HasKey(e => e.Id).HasName("teams_pkey");
            entity.HasIndex(e => e.Name).IsUnique().HasDatabaseName("teams_name_key");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.Country).HasMaxLength(50).HasColumnName("country");
            entity.Property(e => e.Tags).HasColumnType("jsonb").HasColumnName("tags");
            entity.Property(e => e.Createdate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("createdate");            
            entity.Property(e => e.Isdeleted).HasDefaultValue(false).HasColumnName("isdeleted");
        }
    }

}
