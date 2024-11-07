using Backend.Data.Models.Constructions.Specyfication.Painting;

namespace Backend.DTO.Specyfication
{
    public class PaintingSpecificationDetails
    {
        public decimal WallSurfaceArea { get; set; }
        public PaintType PaintType { get; set; }
        public int NumberOfCoats { get; set; }
    }
}
