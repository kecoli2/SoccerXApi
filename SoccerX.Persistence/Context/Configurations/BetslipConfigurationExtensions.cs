using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Context.Configurations
{
    public partial class BetslipConfiguration
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method

        partial void OnConfigurePartial(EntityTypeBuilder<Betslip> entity)
        {
            entity.Property(e => e.Status).HasColumnType("betslipstatus").HasColumnName("status").IsRequired();
        }

        #endregion

        #region Private Method
        #endregion
    }
}
