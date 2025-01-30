using Backend.Data.Models.Constructions;
using Backend.Data.Models.Suppliers;

namespace Backend.DTO.Price
{
    public class MaterialPriceDto
    {
        public int ID { get; set; }
        public MaterialType MaterialType { get; set; }
        public ConstructionType MaterialCategory { get; set; }
        public decimal? PriceWithoutTax { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
    }
}
