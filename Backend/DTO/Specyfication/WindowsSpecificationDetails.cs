using Backend.Data.Models.Constructions.Specyfication.Windows;

namespace Backend.DTO.Specyfication
{
    public class WindowsSpecificationDetails
    {
        public int Amount { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public WindowsMaterial? Material { get; set; }
    }
}
