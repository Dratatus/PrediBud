using Backend.Data.Models.Notifications;

namespace Backend.services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(ConstructionOrderNotification notification);
    }
}
