using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface IReferralRewardRepository : IGenericRepository<Referralrewards>
    {
        Task<IEnumerable<Referralrewards>> GetRewardsByReferrerAsync(Guid referrerId);
    }
}
