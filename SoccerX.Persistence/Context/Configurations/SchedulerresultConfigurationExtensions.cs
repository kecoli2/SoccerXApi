using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Context.Configurations
{
    public partial class SchedulerresultConfiguration
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method

        partial void OnConfigurePartial(EntityTypeBuilder<Schedulerresult> entity)
        {
            entity.Property(e => e.Result).HasColumnName("result").HasColumnType("scheduler_result_enum");
        }

        #endregion

        #region Private Method
        #endregion
    }
}
