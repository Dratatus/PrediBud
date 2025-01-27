namespace Backend.Data.Models.Constructions.Specyfication.Foundation
{
    public class FoundationSpecification: ConstructionSpecification
    {
        public FoundationSpecification()
        {
            Type = ConstructionType.Foundation;
        }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Depth { get; set; }
    }
}
