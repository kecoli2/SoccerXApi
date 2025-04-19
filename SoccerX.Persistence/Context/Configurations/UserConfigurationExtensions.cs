using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Context.Configurations
{
    public partial class UserConfiguration
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method

        partial void OnConfigurePartial(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.Status).HasColumnType("userstatus").HasColumnName("status").IsRequired();
            entity.Property(e => e.Role).HasColumnType("userrole").HasColumnName("role").IsRequired();
            entity.Property(e => e.Gender).HasColumnType("usergender").HasColumnName("gender").IsRequired();
            entity.Property(e => e.Provider).HasColumnType("authprovider").HasColumnName("provider").IsRequired();
        }

        #endregion

        #region Private Method
        #endregion
    }
}
