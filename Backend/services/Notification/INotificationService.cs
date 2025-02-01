using Backend.Data.Models.Notifications;
using Backend.DTO.Notification;

namespace Backend.services.Notification
{
    public interface INotificationService
    {
        Task DeleteAllNotificationsAsync(int userId);
        Task DeleteNotificationAsync(int notificationId);
        Task<IEnumerable<ConstructionOrderNotificationDto>> GetUnreadNotificationsAsync(int userId);
        Task MarkAllAsReadAsync(int userId);
        Task MarkAsReadAsync(int notificationId);
        Task SendNotificationAsync(ConstructionOrderNotification notification);
    }
}
