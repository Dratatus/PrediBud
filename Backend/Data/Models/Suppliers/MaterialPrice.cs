using Backend.Data.Models.Constructions;

namespace Backend.Data.Models.Suppliers
{
    public class MaterialPrice
    {
        public int ID { get; set; }
        public MaterialType MaterialType { get; set; }
        public ConstructionType MaterialCategory { get; set; } 
        public decimal? PriceWithoutTax { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
