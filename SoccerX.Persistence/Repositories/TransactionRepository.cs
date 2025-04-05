using Microsoft.EntityFrameworkCore;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;

namespace SoccerX.Persistence.Repositories
{
    public class TransactionRepository(SoccerXDbContext context) : GenericRepository<Transaction>(context), ITransactionRepository
    {

        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId)
        {
            return await Context.Transactions
                .Where(t => t.Userid == userId)
                .OrderByDescending(t => t.Createdate)
                .ToListAsync();
        }

        public async Task<decimal> GetUserBalanceAsync(Guid userId)
        {
            return await Context.Transactions
                .Where(t => t.Userid == userId)
                .SumAsync(t => t.Amount);
        }
        #endregion

        #region Private Method
        #endregion                   
    }
}
