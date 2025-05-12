using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;

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
            entity.Property(e => e.Status).HasColumnType("userstatus").HasColumnName("status").HasDefaultValue(UserStatus.Active).IsRequired();
            entity.Property(e => e.Role).HasColumnType("userrole").HasColumnName("role").HasDefaultValue(UserRole.User).IsRequired();
            entity.Property(e => e.Gender).HasColumnType("usergender").HasColumnName("gender").IsRequired();
            entity.Property(e => e.Provider).HasColumnType("authprovider").HasColumnName("provider").HasDefaultValue(AuthProvider.Local).IsRequired();
            entity.Property(e => e.Xmin).HasColumnName("xmin").HasColumnType("xid").IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
        }

        #endregion

        #region Private Method
        #endregion
    }
}
