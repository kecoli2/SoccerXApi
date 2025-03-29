using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Interfaces;

namespace SoccerX.Persistence.Repositories
{
    public class NotificationRepository(SoccerXDbContext context) : GenericRepository<Notifications>(context), INotificationRepository
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public async Task<IEnumerable<Notifications>> GetUnreadNotificationsAsync(Guid userId)
        {
            return await _context.Notifications
               .Where(n => n.Userid == userId && !n.Isread && (n.Isdeleted == null || n.Isdeleted == false))
               .OrderByDescending(n => n.Createdate)
               .ToListAsync();
        }

        public async Task MarkAsReadAsync(Guid notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.Isread = true;
                notification.Updatedate = DateTime.UtcNow;
                _context.Notifications.Update(notification);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Private Method
        #endregion        
    }
}
