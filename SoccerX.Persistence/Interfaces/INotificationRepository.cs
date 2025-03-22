using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface INotificationRepository : IGenericRepository<Notifications>
    {
        Task<IEnumerable<Notifications>> GetUnreadNotificationsAsync(Guid userId);
    }
}
