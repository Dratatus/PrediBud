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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
