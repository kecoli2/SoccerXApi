using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;

namespace SoccerX.Persistence.Context
{
    public partial class SoccerXDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Status)
                .HasColumnType("userstatus")                
                .HasColumnName("status")
                .IsRequired();

                entity.Property(e => e.Role)
                .HasColumnType("userrole")
                .HasColumnName("role")
                .IsRequired();
            });
        }
    }
}
