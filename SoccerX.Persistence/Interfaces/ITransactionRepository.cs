using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId);
        Task<decimal> GetUserBalanceAsync(Guid userId);
    }
}
