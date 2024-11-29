using Backend.Data.Models.Notifications;
using Backend.Data.Models.Users;
using Backend.Repositories;
using Backend.services;
using System.Threading.Tasks;

namespace Backend.Services
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
                return;
            }

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
    }
}
