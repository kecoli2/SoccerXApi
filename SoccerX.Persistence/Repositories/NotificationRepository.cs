using Microsoft.EntityFrameworkCore;
using SoccerX.Application.Interfaces.Repository;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;

namespace SoccerX.Persistence.Repositories
{
    public class NotificationRepository(SoccerXDbContext context) : GenericRepository<Notification>(context), INotificationRepository
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(Guid userId)
        {
            return await Context.Notifications
               .Where(n => n.Userid == userId && !n.Isread && (n.Isdeleted == null || n.Isdeleted == false))
               .OrderByDescending(n => n.Createdate)
               .ToListAsync();
        }

        public async Task MarkAsReadAsync(Guid notificationId)
        {
            var notification = await Context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.Isread = true;
                notification.Updatedate = DateTime.UtcNow;
                Context.Notifications.Update(notification);
                await Context.SaveChangesAsync();
            }
        }
        #endregion

        #region Private Method
        #endregion        
    }
}
