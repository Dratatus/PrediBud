using Backend.Data.Models.Suppliers;

namespace Backend.Data.Models.MaterialPrices.Stairs
{
    public class StaircaseMaterialPrice : MaterialPrice
    {
        public decimal PricePerStep { get; set; } 
        public decimal? StandardStepHeight { get; set; }
        public decimal? StandardStepWidth { get; set; }
    }
}
