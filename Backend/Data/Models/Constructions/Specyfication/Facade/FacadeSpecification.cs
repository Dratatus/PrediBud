using Backend.Data.Models.Constructions.Specyfication;

namespace Backend.Data.Models.Constructions.Dimensions.Facade
{
    public class FacadeSpecification : ConstructionSpecification
    {
        public FacadeSpecification()
        {
            Type = ConstructionType.Facade;
        }
        public decimal SurfaceArea { get; set; }  
        public InsulationType InsulationType { get; set; }  
        public FinishMaterial FinishMaterial { get; set; }
    }
}
