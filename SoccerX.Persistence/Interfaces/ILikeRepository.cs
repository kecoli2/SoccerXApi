using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface ILikeRepository : IGenericRepository<Likes>
    {
        Task<int> CountLikesByBetSlipIdAsync(Guid betSlipId);
    }
}
