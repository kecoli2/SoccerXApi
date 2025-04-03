using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByBetSlipIdAsync(Guid betSlipId);
    }
}
