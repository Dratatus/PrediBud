using Backend.Data.Models.Suppliers;

namespace Backend.Data.Models.MaterialPrices.Painting
{
    public class PaintingMaterialPrice: MaterialPrice
    {
        public decimal PricePerLiter { get; set; }
        public decimal CoveragePerLiter { get; set; }
    }
}
