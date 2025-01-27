using Backend.Data.Models.Constructions.Dimensions;

namespace Backend.Data.Models.Constructions.Specyfication.Painting
{
    public class PaintingSpecification : ConstructionSpecification
    {
        public PaintingSpecification()
        {
            Type = ConstructionType.Painting;
        }
        public decimal WallSurfaceArea { get; set; }
        public PaintType PaintType { get; set; } 
        public int NumberOfCoats { get; set; } 
    }
}
