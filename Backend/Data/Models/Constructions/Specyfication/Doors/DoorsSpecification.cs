using Backend.Data.Models.Constructions.Specyfication;

namespace Backend.Data.Models.Constructions.Dimensions.Doors
{
    public class DoorsSpecification : ConstructionSpecification
    {
        public DoorsSpecification()
        {
            Type = ConstructionType.Doors;
        }
        public int Amount { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public DoorMaterial Material { get; set; }
    }
}
