using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Context.Configurations
{
    public partial class PaymentConfiguration
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method

        partial void OnConfigurePartial(EntityTypeBuilder<Payment> entity)
        {
            entity.Property(e => e.PaymentStatus).HasColumnName("paymentstatus").HasColumnType("paymentstatus");
            entity.Property(e => e.PaymentMethod).HasColumnName("paymentmethod").HasColumnType("paymentmethod");
        }

        #endregion

        #region Private Method
        #endregion
    }
}
