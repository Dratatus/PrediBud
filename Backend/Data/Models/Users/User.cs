using Backend.Data.Models.Common;
using Backend.Data.Models.Credidentials;
using Backend.Data.Models.Notifications;
using Backend.Data.Models.Orders.Material;

namespace Backend.Data.Models.Users
{
    public class User
    {
        public int ID { get; set; }
        public ContactDetails ContactDetails { get; set; }
        public Credentials Credentials { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public List<ConstructionOrderNotification> ConstructionOrderNotifications { get; set; } = new List<ConstructionOrderNotification>();
        public List<MaterialOrder> MaterialOrders { get; set; } = new List<MaterialOrder>();
    }
}
