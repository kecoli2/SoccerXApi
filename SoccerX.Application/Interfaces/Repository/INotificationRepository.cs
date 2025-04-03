using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoccerX.Domain.Entities;

namespace SoccerX.Application.Interfaces.Repository;

public interface INotificationRepository : IGenericRepository<Notification>
{
    Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(Guid userId);
}