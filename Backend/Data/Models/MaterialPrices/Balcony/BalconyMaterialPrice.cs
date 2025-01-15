using Backend.Data.Models.Suppliers;

namespace Backend.Data.Models.MaterialPrices.Balcony
{
    public class BalconyMaterialPrice: MaterialPrice
    {
        public decimal Height { get; set; }
        public decimal PricePerMeter { get; set; }
    }
}
