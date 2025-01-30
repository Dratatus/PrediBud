using Backend.Data.Models.Users;

namespace Backend.Data.Models.Notifications
{
    public class ConstructionOrderNotification
    {
        public int ID { get; set; }
        public NotificationStatus Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? WorkerId { get; set; }
        public Worker Worker { get; set; }
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        public int ConstructionOrderID { get; set; }
        public DateTime Date { get; set; }
    }
}
