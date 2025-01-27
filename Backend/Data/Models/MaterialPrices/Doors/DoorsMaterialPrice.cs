using Backend.Data.Models.Suppliers;

namespace Backend.Data.Models.MaterialPrices.Doors
{
    public class DoorsMaterialPrice : MaterialPrice
    {
        public decimal Height { get; set; }  
        public decimal Width { get; set; }  
        public decimal PricePerDoor { get; set; }
    }
}
