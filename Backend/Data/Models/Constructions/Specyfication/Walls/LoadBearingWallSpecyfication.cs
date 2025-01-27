namespace Backend.Data.Models.Constructions.Specyfication.Walls
{
    public class LoadBearingWallSpecification: ConstructionSpecification
    {
        public LoadBearingWallSpecification()
        {
            Type = ConstructionType.LoadBearingWall;
        }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Thickness { get; set; }
        public LoadBearingWallMaterial Material { get; set; }
    }
}
