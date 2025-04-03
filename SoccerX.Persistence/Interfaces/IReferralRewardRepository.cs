using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface IReferralRewardRepository : IGenericRepository<Referralreward>
    {
        Task<IEnumerable<Referralreward>> GetRewardsByReferrerAsync(Guid referrerId);
    }
}
