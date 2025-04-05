using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using SoccerX.Application.Interfaces.Repository;

namespace SoccerX.Persistence.Repositories
{
    public class ReferralRewardRepository(SoccerXDbContext context) : GenericRepository<Referralreward>(context), IReferralRewardRepository
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<IEnumerable<Referralreward>> GetRewardsByReferrerAsync(Guid referrerId)
        {
            return await Context.Referralrewards
                .Where(r => r.Referrerid == referrerId)
                .ToListAsync();
        }
        #endregion

        #region Private Method
        #endregion

    }
}
