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
        public decimal Area { get; set; }  
        public InsulationMaterial Material { get; set; } 
        public decimal Thickness { get; set; }  
    }
}
