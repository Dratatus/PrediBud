namespace Backend.Data.Models.Constructions.Dimensions
{
    public class WindowsSpecification: IConstructionDimensions
    {
        public ConstructionType Type => ConstructionType.Windows;
        public int Amount { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
    }
}
