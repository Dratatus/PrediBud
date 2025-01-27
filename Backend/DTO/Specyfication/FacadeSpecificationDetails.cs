using Backend.Data.Models.Constructions.Dimensions.Facade;

namespace Backend.DTO.Specyfication
{
    public class FacadeSpecificationDetails
    {
        public decimal SurfaceArea { get; set; }
        public InsulationType? InsulationType { get; set; }
        public FinishMaterial? FinishMaterial { get; set; }
    }
}
