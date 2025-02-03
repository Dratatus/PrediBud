using Backend.Data.Consts;
using Backend.Data.Models.Notifications;
using Backend.Data.Models.Users;
using Backend.DTO.Notification;
using Backend.Middlewares;
using Backend.Repositories;
using System.Threading.Tasks;

namespace Backend.services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IConstructionOrderNotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;

        public NotificationService(IConstructionOrderNotificationRepository notificationRepository, IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        private ConstructionOrderNotificationDto MapToDto(ConstructionOrderNotification notification)
        {
            return new ConstructionOrderNotificationDto
            {
                ID = notification.ID,
                Status = notification.Status,
                Title = notification.Title,
                Description = notification.Description,
                IsRead = notification.IsRead,
                ConstructionOrderID = notification.ConstructionOrderID,
                Date = notification.Date
            };
        }

        public async Task SendNotificationAsync(ConstructionOrderNotification notification)
        {
            User user = null;

            if (notification.ClientId > 0)
            {
                user = await _userRepository.GetUserByIdAsync(notification.ClientId.Value);
            }
            else if (notification.WorkerId > 0)
            {
                user = await _userRepository.GetUserByIdAsync(notification.WorkerId.Value);
            }

            if (user == null)
            {
                throw new ApiException(ErrorMessages.UserNotFound, StatusCodes.Status404NotFound);
            }

            notification.IsRead = false;

            if (user is Client client)
            {
                client.ConstructionOrderNotifications.Add(notification);
            }
            else if (user is Worker worker)
            {
                worker.ConstructionOrderNotifications.Add(notification);
            }

            await _notificationRepository.AddAsync(notification);
            await _notificationRepository.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);

            if (notification == null)
            {
                throw new ApiException(ErrorMessages.NotificationNotFound, StatusCodes.Status404NotFound);
            }

            notification.IsRead = true;
            await _notificationRepository.SaveChangesAsync();
        }

        public async Task MarkAllAsReadAsync(int userId)
        {
            var notifications = await _notificationRepository.GetNotificationsByUserIdAsync(userId);

            if (!notifications.Any())
            {
                throw new ApiException(ErrorMessages.NotificationsNotFound, StatusCodes.Status404NotFound);
            }

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _notificationRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ConstructionOrderNotificationDto>> GetUnreadNotificationsAsync(int userId)
        {
            var notifications = await _notificationRepository.GetNotificationsByUserIdAsync(userId);
            return notifications.Select(MapToDto);
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);

            if (notification == null)
            {
                throw new ApiException(ErrorMessages.NotificationNotFound, StatusCodes.Status404NotFound);
            }

            await _notificationRepository.DeleteAsync(notification);
            await _notificationRepository.SaveChangesAsync();
        }

        public async Task DeleteAllNotificationsAsync(int userId)
        {
            var notifications = await _notificationRepository.GetNotificationsByUserIdAsync(userId);

            await _notificationRepository.DeleteAllAsync(notifications);
            await _notificationRepository.SaveChangesAsync();
        }
    }
}
