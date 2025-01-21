using Backend.Data.Models.Constructions;
using Backend.Data.Models.Suppliers;
using Backend.Data.Models.Users;

namespace Backend.Data.Models.Orders.Material
{
    public class MaterialOrder
    {
        public int ID { get; set; }
        public decimal UnitPriceNet { get; set; }
        public decimal UnitPriceGross { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPriceNet => UnitPriceNet * Quantity;
        public decimal TotalPriceGross => UnitPriceGross * Quantity;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public User User { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int? MaterialPriceId { get; set; }
        public MaterialPrice MaterialPrice { get; set; }
    }
}
