using Backend.Data.Models.Notifications;
using Backend.Data.Models.Users;

namespace Backend.Data.Models.Orders
{
    public class ConstructionOrder
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int? WorkerId { get; set; }
        public Worker Worker { get; set; }

        public List<ConstructionOrderNotification> Notifications { get; set; } = new List<ConstructionOrderNotification>();
    }
}
