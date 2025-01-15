using Backend.Data.Models.Suppliers;

namespace Backend.Data.Models.MaterialPrices.Insulation
{
    public class InsulationOfAtticMaterialPrice: MaterialPrice
    {
        public decimal PricePerSquareMeter { get; set; }
        public decimal Thickness { get; set; }
    }
}
