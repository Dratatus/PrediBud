using Backend.Data.Models.Constructions.Dimensions;

namespace Backend.Data.Models.Constructions.Specyfication.Stairs
{
    public class StaircaseSpecification : ConstructionSpecification
    {
        public StaircaseSpecification()
        {
            Type = ConstructionType.Staircase;
        }
        public int NumberOfSteps { get; set; }  // Liczba stopni
        public decimal Height { get; set; }  // Wysokość stopnia
        public decimal Width { get; set; }  // Szerokość stopnia
        public StaircaseMaterial Material { get; set; }  // Materiał schodów
    }
}
