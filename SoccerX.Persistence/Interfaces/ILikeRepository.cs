using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface ILikeRepository : IGenericRepository<Like>
    {
        Task<int> CountLikesByBetSlipIdAsync(Guid betSlipId);
    }
}
