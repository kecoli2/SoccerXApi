using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Context.Configurations
{
    public partial class TransactionConfiguration
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method

        partial void OnConfigurePartial(EntityTypeBuilder<Transaction> entity)
        {
            entity.Property(e => e.TransactionType).HasColumnType("transactiontype").HasColumnName("transactiontype").IsRequired();
        }

        #endregion

        #region Private Method
        #endregion
    }
}
