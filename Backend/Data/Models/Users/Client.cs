using Backend.Data.Models.Orders.Construction;
using Backend.Data.Models.Orders.Material;

namespace Backend.Data.Models.Users
{
    public class Client : User
    {
        public List<ConstructionOrder> ConstructionOrders { get; set; } = new List<ConstructionOrder>();
        
    }

}
