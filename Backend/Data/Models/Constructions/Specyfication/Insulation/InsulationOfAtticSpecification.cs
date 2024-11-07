using Backend.Data.Models.Constructions.Specyfication;
using Backend.Data.Models.Constructions.Specyfication.Insulation;

namespace Backend.Data.Models.Constructions.Dimensions
{
    public class InsulationOfAtticSpecification: ConstructionSpecification
    {
        public InsulationOfAtticSpecification()
        {
            Type = ConstructionType.InsulationOfAttic;
        }
        public decimal Area { get; set; }  // Powierzchnia poddasza w metrach kwadratowych
        public InsulationMaterial Material { get; set; }  // Materiał izolacyjny (np. wełna mineralna, styropian)
        public decimal Thickness { get; set; }  // Grubość izolacji w centymetrach
    }
}
