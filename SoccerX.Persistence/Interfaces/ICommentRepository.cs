using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comments>
    {
        Task<IEnumerable<Comments>> GetCommentsByBetSlipIdAsync(Guid betSlipId);
    }
}
