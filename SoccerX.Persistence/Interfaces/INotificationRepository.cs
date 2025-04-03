using SoccerX.Domain.Entities;

namespace SoccerX.Persistence.Interfaces
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(Guid userId);
    }
}
