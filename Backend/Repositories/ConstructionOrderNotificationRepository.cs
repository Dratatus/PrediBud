using Backend.Data;
using Backend.Data.Models.Notifications;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Data.Context;

namespace Backend.Repositories
{
    public class ConstructionOrderNotificationRepository : IConstructionOrderNotificationRepository
    {
        private readonly PrediBudDBContext _context;

        public ConstructionOrderNotificationRepository(PrediBudDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ConstructionOrderNotification notification)
        {
            await _context.ConstructionOrderNotifications.AddAsync(notification);
        }

        public async Task<ConstructionOrderNotification> GetByIdAsync(int notificationId)
        {
            return await _context.ConstructionOrderNotifications
                .FirstOrDefaultAsync(n => n.ID == notificationId);
        }

        public async Task<IEnumerable<ConstructionOrderNotification>> GetNotificationsByUserIdAsync(int userId)
        {
            return await _context.ConstructionOrderNotifications
                .Where(n => n.ClientId == userId || n.WorkerId == userId)
                .OrderByDescending(n => n.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<ConstructionOrderNotification>> GetUnreadNotificationsByUserIdAsync(int userId)
        {
            return await _context.ConstructionOrderNotifications
                .Where(n => (n.ClientId == userId || n.WorkerId == userId) && !n.IsRead)
                .OrderByDescending(n => n.Date)
                .ToListAsync();
        }

        public async Task DeleteAsync(ConstructionOrderNotification notification)
        {
            _context.ConstructionOrderNotifications.Remove(notification);
        }

        public async Task DeleteAllAsync(IEnumerable<ConstructionOrderNotification> notifications)
        {
            _context.ConstructionOrderNotifications.RemoveRange(notifications);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}