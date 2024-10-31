namespace Backend.Data.Models.Constructions.Dimensions.Doors
{
    public class DoorsSpecification : IConstructionDimensions
    {
        public ConstructionType Type => ConstructionType.Doors;
        public int Amount { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public DoorMaterial Material { get; set; }
    }
}
