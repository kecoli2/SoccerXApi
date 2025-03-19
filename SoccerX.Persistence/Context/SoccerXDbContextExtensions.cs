using Microsoft.EntityFrameworkCore;
using Npgsql.NameTranslation;
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
            modelBuilder.HasPostgresEnum<UserStatus>(name: "userstatus");           
            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Status)
                .HasColumnType("userstatus")                
                .HasColumnName("status");
            });
        }
    }
}
