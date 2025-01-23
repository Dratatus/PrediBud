namespace Backend.Data.Models.Constructions.Specyfication.Walls
{
    public class PartitionWallSpecification: ConstructionSpecification
    {
        public PartitionWallSpecification()
        {
            Type = ConstructionType.LoadBearingWall;
        }
        public decimal? Height { get; set; }
        public decimal? Width { get; set; }
        public decimal? Thickness { get; set; }
        public PartitionWallMaterial Material { get; set; }
    }
}
