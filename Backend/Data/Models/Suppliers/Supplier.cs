using Backend.Data.Models.Common;
using Backend.Data.Models.Orders.Material;

namespace Backend.Data.Models.Suppliers
{
    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; } 
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public string ContactEmail { get; set; }
        public List<MaterialOrder> MaterialOrders { get; set; } = new List<MaterialOrder>();
        public List<MaterialPrice> MaterialPrices { get; set; } = new List<MaterialPrice>();
    }
}
