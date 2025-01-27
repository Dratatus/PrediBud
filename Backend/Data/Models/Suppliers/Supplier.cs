using Backend.Data.Models.Common;
using Backend.Data.Models.Orders.Material;
using System.Text.Json.Serialization;

namespace Backend.Data.Models.Suppliers
{
    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; } 
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public string ContactEmail { get; set; }

        [JsonIgnore]
        public List<MaterialOrder> MaterialOrders { get; set; } = new List<MaterialOrder>();

        [JsonIgnore]
        public List<MaterialPrice> MaterialPrices { get; set; } = new List<MaterialPrice>();
    }
}
