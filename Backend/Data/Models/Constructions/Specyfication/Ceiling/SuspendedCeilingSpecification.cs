using Backend.Data.Models.Constructions.Dimensions;

namespace Backend.Data.Models.Constructions.Specyfication.Ceiling
{
    public class SuspendedCeilingSpecification : IConstructionDimensions
    {
        public ConstructionType Type => ConstructionType.SuspendedCeiling;
        public decimal Area { get; set; }  // Powierzchnia sufitu w metrach kwadratowych
        public decimal Height { get; set; }  // Wysokość sufitu od podłogi
        public string Material { get; set; }  // Materiał sufitu podwieszanego (np. płyty gipsowo-kartonowe)
    }
}
