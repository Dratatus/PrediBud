using Backend.Data.Models.Constructions.Dimensions;

namespace Backend.Data.Models.Constructions.Specyfication.Stairs
{
    public class StaircaseSpecification : ConstructionSpecification
    {
        public StaircaseSpecification()
        {
            Type = ConstructionType.Staircase;
        }
        public int NumberOfSteps { get; set; } 
        public decimal Height { get; set; }
        public decimal Width { get; set; }  
        public StaircaseMaterial Material { get; set; }  
    }
}
