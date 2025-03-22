using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payments>
    {
        Task<IEnumerable<Payments>> GetPaymentsByUserIdAsync(Guid userId);
        Task<Payments?> GetLastSuccessfulPaymentAsync(Guid userId);
    }
}
