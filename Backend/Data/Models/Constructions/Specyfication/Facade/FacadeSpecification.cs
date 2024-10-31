namespace Backend.Data.Models.Constructions.Dimensions.Facade
{
    public class FacadeSpecification : IConstructionDimensions
    {
        public ConstructionType Type => ConstructionType.Facade;
        public decimal SurfaceArea { get; set; }  // Powierzchnia elewacji w metrach kwadratowych
        public InsulationType InsulationType { get; set; }  // Typ izolacji (np. styropian, wełna mineralna)
        public FinishMaterial FinishMaterial { get; set; }
    }
}
