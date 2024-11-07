using Backend.Data.Models.Constructions.Dimensions;

namespace Backend.Data.Models.Constructions.Specyfication
{
    public abstract class ConstructionSpecification : IConstructionSpecification
    {
        public int ID { get; set; }  
        public ConstructionType Type { get; set; } 
    }
}
