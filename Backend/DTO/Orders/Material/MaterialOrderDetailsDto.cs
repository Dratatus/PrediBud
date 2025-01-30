using Backend.DTO.Price;
using Backend.DTO.Users.Supplier;

namespace Backend.DTO.Orders.Material
{
    public class MaterialOrderDetailsDto
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
        public SupplierDto Supplier { get; set; } 
        public int? MaterialPriceId { get; set; }
        public MaterialPriceDto MaterialPrice { get; set; } 
        public OrderAddressDto Address { get; set; }
    }
}
