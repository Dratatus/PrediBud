using Backend.Data.Models.Notifications;
using Backend.Data.Models.Orders;

namespace Backend.Data.Models.Users
{
    public class Worker : User
    {
        public string Position { get; set; }
        public List<ConstructionOrderNotification> ConstructionOrderNotifications { get; set; } = new List<ConstructionOrderNotification>();
        public List<ConstructionOrder> ConstructionOrders { get; set; } = new List<ConstructionOrder>();
    }
}
