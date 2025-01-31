using Backend.Data.Models.Suppliers;
using Backend.DTO.Users.Supplier;

namespace Backend.DTO.Orders.Material
{
    public class MaterialOrderDto
    {
        public int ID { get; set; }
        public decimal UnitPriceNet { get; set; }
        public decimal UnitPriceGross { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPriceNet { get; set; }
        public decimal TotalPriceGross { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int SupplierId { get; set; }
        public int? MaterialPriceId { get; set; }
        public OrderAddressDto Address { get; set; }
    }
}
