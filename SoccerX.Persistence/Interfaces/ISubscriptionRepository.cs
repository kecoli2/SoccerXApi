using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface ISubscriptionRepository : IGenericRepository<Subscriptions>
    {
        Task<IEnumerable<Subscriptions>> GetActiveSubscriptionsBySubscriberAsync(Guid subscriberId);
        Task<bool> HasActiveSubscriptionAsync(Guid subscriberId, Guid editorId);
    }
}
