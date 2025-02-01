using Backend.Data.Models.Suppliers;
using Backend.DTO.Users.Supplier;

namespace Backend.DTO.Orders.Material
{
    public class UpdateMaterialOrderDto
    {
        public int ID { get; set; }

        public decimal UnitPriceNet { get; set; }
        public decimal UnitPriceGross { get; set; }
        public decimal Quantity { get; set; }
        public int SupplierId { get; set; }
        public int? MaterialPriceId { get; set; }
        public OrderAddressDto Address { get; set; }
    }
}
