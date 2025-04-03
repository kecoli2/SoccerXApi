using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(Guid userId);
        Task<Payment?> GetLastSuccessfulPaymentAsync(Guid userId);
    }
}
