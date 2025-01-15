using Backend.Data.Models.Constructions.Specyfication;

namespace Backend.Data.Models.Constructions.Specyfication.Windows
{
    public class WindowsSpecification : ConstructionSpecification
    {
        public WindowsSpecification()
        {
            Type = ConstructionType.Windows;
        }
        public int Amount { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public WindowsMaterial Material { get; set; }
    }
}
