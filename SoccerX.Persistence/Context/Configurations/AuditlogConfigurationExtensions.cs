using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Context.Configurations
{
    public partial class AuditlogConfiguration
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method

        partial void OnConfigurePartial(EntityTypeBuilder<Auditlog> entity)
        {
            entity.Property(e => e.Action).HasColumnType("auditaction").HasColumnName("action").IsRequired();
        }

        #endregion

        #region Private Method
        #endregion
    }
}
