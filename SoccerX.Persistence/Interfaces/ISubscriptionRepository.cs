using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface ISubscriptionRepository : IGenericRepository<Subscription>
    {
        Task<IEnumerable<Subscription>> GetActiveSubscriptionsBySubscriberAsync(Guid subscriberId);
        Task<bool> HasActiveSubscriptionAsync(Guid subscriberId, Guid editorId);
    }
}
