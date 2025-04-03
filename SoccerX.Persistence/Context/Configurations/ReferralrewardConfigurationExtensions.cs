using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Context.Configurations
{
    public partial class ReferralrewardConfiguration
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method

        partial void OnConfigurePartial(EntityTypeBuilder<Referralreward> entity)
        {
            entity.Property(e => e.Status).HasColumnName("status").HasColumnType("referralstatus");
        }

        #endregion

        #region Private Method
        #endregion
    }
}
