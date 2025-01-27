using Backend.Data.Models.Constructions.Specyfication;

namespace Backend.Data.Models.Constructions.Dimensions.Floor
{
    public class FlooringSpecification : ConstructionSpecification
    {
        public FlooringSpecification()
        {
            Type = ConstructionType.Flooring;
        }
        public decimal Area { get; set; }
        public FlooringMaterial Material { get; set; } 
    }
}
