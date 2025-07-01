using System;

namespace SoccerX.DTO.Dto
{
    public class TransactionResultDto
    {
        #region Field
        public Guid TransactionId { get; set; }
        public decimal OldBalance { get; set; }
        public decimal NewBalance { get; set; }
        public DateTime Timestamp { get; set; }
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        #endregion

        #region Private Method
        #endregion        
    }
}
