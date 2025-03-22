using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SoccerX.Persistence.Repositories
{
    public class ReferralRewardRepository(SoccerXDbContext context) : GenericRepository<Referralrewards>(context), IReferralRewardRepository
    {
        public async Task<IEnumerable<Referralrewards>> GetRewardsByReferrerAsync(Guid referrerId)
        {
            return await _context.Referralrewards
                .Where(r => r.Referrerid == referrerId)
                .ToListAsync();
        }
    }
}
