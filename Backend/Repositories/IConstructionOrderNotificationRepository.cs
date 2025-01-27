using Backend.Data.Models.Notifications;

namespace Backend.Repositories
{
    public interface IConstructionOrderNotificationRepository
    {
        Task AddAsync(ConstructionOrderNotification notification);
        Task SaveChangesAsync();
    }
}
