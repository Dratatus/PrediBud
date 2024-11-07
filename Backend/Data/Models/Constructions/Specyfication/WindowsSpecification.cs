using Backend.Data.Models.Constructions.Specyfication;

namespace Backend.Data.Models.Constructions.Dimensions
{
    public class WindowsSpecification: ConstructionSpecification
    {
        public WindowsSpecification()
        {
            Type = ConstructionType.Windows;
        }
        public int Amount { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
    }
}
