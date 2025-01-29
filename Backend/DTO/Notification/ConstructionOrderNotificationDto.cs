using Backend.Data.Models.Notifications;

namespace Backend.DTO.Notification
{
    public class ConstructionOrderNotificationDto
    {
        public int ID { get; set; }
        public NotificationStatus Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public int ConstructionOrderID { get; set; }
        public DateTime Date { get; set; }
    }
}
