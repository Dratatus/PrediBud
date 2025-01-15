using Backend.Data.Models.Suppliers;

namespace Backend.Data.Models.MaterialPrices.Facade
{
    public class FacadeMaterialPrice: MaterialPrice
    {
        public decimal Thickness { get; set; } 
        public decimal PricePerSquareMeter { get; set; } 
        public decimal PricePerSquareMeterFinish { get; set; } 
    }
}
