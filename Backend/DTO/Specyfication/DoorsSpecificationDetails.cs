using Backend.Data.Models.Constructions.Dimensions.Doors;

namespace Backend.DTO.Specyfication
{
    public class DoorsSpecificationDetails
    {
        public int Amount { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public DoorMaterial? Material { get; set; }
    }
}
