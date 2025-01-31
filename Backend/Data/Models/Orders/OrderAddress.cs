using Backend.Data.Models.Orders.Construction;
using Backend.Data.Models.Orders.Material;

namespace Backend.Data.Models.Orders
{
    public class OrderAddress
    {
        public int ID { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string StreetName { get; set; }
        public int? ConstructionOrderId { get; set; }
        public ConstructionOrder ConstructionOrder { get; set; }
        public int? MaterialOrderId { get; set; }
        public MaterialOrder MaterialOrder { get; set; }
    }
}
