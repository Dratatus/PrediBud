using Backend.Data.Models.Common;
using Backend.Data.Models.Credidentials;
using Backend.Data.Models.Orders;
using Backend.Data.Models.Suppliers;

namespace Backend.Data.Models.Users
{
    public class Client : User
    {
        public List<ConstructionOrder> ConstructionOrders { get; set; } = new List<ConstructionOrder>();
        public List<MaterialOrder> MaterialOrders { get; set; } = new List<MaterialOrder>();
    }

}
