using Backend.Data.Models.Constructions.Dimensions;

namespace Backend.Data.Models.Constructions.Specyfication.Painting
{
    public class PaintingSpecification : IConstructionDimensions
    {
        public ConstructionType Type => ConstructionType.Painting;
        public decimal WallSurfaceArea { get; set; }  // Powierzchnia ścian do malowania
        public PaintType PaintType { get; set; }  // Typ farby (np. akrylowa, lateksowa)
        public int NumberOfCoats { get; set; }  // Liczba warstw farby
    }
}
