using Backend.Data.Models.Notifications;

namespace Backend.Repositories
{
    public interface IConstructionOrderNotificationRepository
    {
        Task AddAsync(ConstructionOrderNotification notification);
        Task DeleteAllAsync(IEnumerable<ConstructionOrderNotification> notifications);
        Task DeleteAsync(ConstructionOrderNotification notification);
        Task<ConstructionOrderNotification> GetByIdAsync(int notificationId);
        Task<IEnumerable<ConstructionOrderNotification>> GetNotificationsByUserIdAsync(int userId);
        Task<IEnumerable<ConstructionOrderNotification>> GetUnreadNotificationsByUserIdAsync(int userId);
        Task SaveChangesAsync();
    }
}
