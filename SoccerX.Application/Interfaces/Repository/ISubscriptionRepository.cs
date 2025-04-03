using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Interfaces.Repository;

public interface ISubscriptionRepository : IGenericRepository<Subscription>
{
    Task<IEnumerable<Subscription>> GetActiveSubscriptionsBySubscriberAsync(Guid subscriberId);
    Task<bool> HasActiveSubscriptionAsync(Guid subscriberId, Guid editorId);
}