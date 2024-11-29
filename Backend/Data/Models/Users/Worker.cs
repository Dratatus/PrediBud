using Backend.Data.Models.Orders;

namespace Backend.Data.Models.Users
{
    public class Worker : User
    {
        public string Position { get; set; }
        public List<ConstructionOrder> AssignedOrders { get; set; } = new List<ConstructionOrder>();
    }
}
