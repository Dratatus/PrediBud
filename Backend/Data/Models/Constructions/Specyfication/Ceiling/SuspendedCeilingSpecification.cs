using Backend.Data.Models.Constructions.Dimensions;

namespace Backend.Data.Models.Constructions.Specyfication.Ceiling
{
    public class SuspendedCeilingSpecification : ConstructionSpecification
    {
        public SuspendedCeilingSpecification()
        {
            Type = ConstructionType.SuspendedCeiling;
        }
        public decimal Area { get; set; }  // Powierzchnia sufitu w metrach kwadratowych
        public decimal Height { get; set; }  // Wysokość sufitu od podłogi
        public SuspendedCeilingMaterial Material { get; set; }  // Materiał sufitu podwieszanego (np. płyty gipsowo-kartonowe)
    }
}
