using Backend.Data.Models.Constructions.Dimensions;

namespace Backend.Data.Models.Constructions.Specyfication.Ceiling
{
    public class SuspendedCeilingSpecification : ConstructionSpecification
    {
        public SuspendedCeilingSpecification()
        {
            Type = ConstructionType.SuspendedCeiling;
        }
        public decimal Area { get; set; }  
        public decimal Height { get; set; }  
        public SuspendedCeilingMaterial Material { get; set; } 
    }
}
