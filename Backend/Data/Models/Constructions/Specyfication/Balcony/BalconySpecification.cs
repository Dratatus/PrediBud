using Backend.Data.Models.Constructions.Specyfication;

namespace Backend.Data.Models.Constructions.Dimensions.Balcony
{
    public class BalconySpecification : ConstructionSpecification
    {
        public BalconySpecification()
        {
            Type = ConstructionType.Balcony;
        }
        public decimal Length { get; set; }  
        public decimal Width { get; set; }  
        public RailingMaterial RailingMaterial { get; set; }  
    }
}
