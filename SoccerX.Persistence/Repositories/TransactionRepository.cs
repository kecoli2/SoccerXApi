using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interfaces;

namespace SoccerX.Persistence.Repositories
{
    public class TransactionRepository(SoccerXDbContext context) : GenericRepository<Transactions>(context), ITransactionRepository
    {
        public async Task<IEnumerable<Transactions>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Transactions
                .Where(t => t.Userid == userId)
                .OrderByDescending(t => t.Createdate)
                .ToListAsync();
        }

        public async Task<decimal> GetUserBalanceAsync(Guid userId)
        {
            return await _context.Transactions
                .Where(t => t.Userid == userId)
                .SumAsync(t => t.Amount);
        }
    }
}
