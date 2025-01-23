using Backend.Data.Models.Suppliers;

namespace Backend.Data.Models.MaterialPrices.Windows
{
    public class WindowsMaterialPrice : MaterialPrice
    {
        public decimal PricePerSquareMeter { get; set; } 
        public decimal StandardHeight { get; set; } 
        public decimal StandardWidth { get; set; } 
    }
}
