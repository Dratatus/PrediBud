using Backend.Data.Models.Notifications;

namespace Backend.services.Notification
{
    public interface INotificationService
    {
        Task SendNotificationAsync(ConstructionOrderNotification notification);
    }
}
