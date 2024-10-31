namespace Backend.Data.Models.Constructions.Dimensions.Balcony
{
    public class BalconySpecification : IConstructionDimensions
    {
        public ConstructionType Type => ConstructionType.Balcony;
        public decimal BalconyLength { get; set; }  
        public decimal BalconyWidth { get; set; }  
        public RailingMaterial RailingMaterial { get; set; }  
    }
}
