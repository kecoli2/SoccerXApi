using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using SoccerX.Application.Interfaces.Repository;

namespace SoccerX.Persistence.Repositories
{
    public class PaymentRepository(SoccerXDbContext context) : GenericRepository<Payment>(context), IPaymentRepository
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(Guid userId)
        {
            return await _context.Payments
                .Where(p => p.Userid == userId)
                .ToListAsync();
        }

        public async Task<Payment?> GetLastSuccessfulPaymentAsync(Guid userId)
        {
            return await _context.Payments
                .Where(p => p.Userid == userId && p.PaymentStatus == Domain.Enums.PaymentStatus.Completed)
                .OrderByDescending(p => p.Paymentdate)
                .FirstOrDefaultAsync();
        }
        #endregion

        #region Private Method
        #endregion                    
    }
}
