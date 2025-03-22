using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface ITransactionRepository : IGenericRepository<Transactions>
    {
        Task<IEnumerable<Transactions>> GetByUserIdAsync(Guid userId);
        Task<decimal> GetUserBalanceAsync(Guid userId);
    }
}
