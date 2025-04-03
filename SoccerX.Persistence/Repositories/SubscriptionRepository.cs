using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SoccerX.Persistence.Repositories
{
    public class SubscriptionRepository(SoccerXDbContext context) : GenericRepository<Subscription>(context), ISubscriptionRepository
    {

        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<IEnumerable<Subscription>> GetActiveSubscriptionsBySubscriberAsync(Guid subscriberId)
        {
            return await _context.Subscriptions
                .Where(s => s.Subscriberid == subscriberId && s.Isactive)
                .ToListAsync();
        }

        public async Task<bool> HasActiveSubscriptionAsync(Guid subscriberId, Guid editorId)
        {
            return await _context.Subscriptions
                .AnyAsync(s => s.Subscriberid == subscriberId && s.Editorid == editorId && s.Isactive);
        }
        #endregion

        #region Private Method
        #endregion                   
    }
}
