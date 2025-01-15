using Backend.Data.Models.Suppliers;

namespace Backend.Data.Models.MaterialPrices.Celling
{
    public class SuspendedCeilingMaterialPrice: MaterialPrice
    {
        public decimal PricePerSquareMeter { get; set; } 
        public decimal MaxHeight { get; set; } = 3.0m;
    }
}
