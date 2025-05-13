using SoccerX.Domain.Enums;
using System;

namespace SoccerX.DTO.Requests.Admin
{
    public class RequestUserAddAmountOutUserAmount
    {
        #region Field
        public Guid FromUser { get; set; }
        public Guid ToUser { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amout { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion
    }
}
