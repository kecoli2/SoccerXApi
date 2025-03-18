using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Infrastructure.Data
{
    public partial class SoccerXDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<UserRole>("userrole");
            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Role)
                .HasColumnName("role")
                .HasColumnType("userrole");
            });
        }
    }
}
