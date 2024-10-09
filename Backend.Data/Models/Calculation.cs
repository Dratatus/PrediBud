using Backend.Data.Models.Suppliers;

namespace Backend.Data.Models
{
    public class Calculation
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public StructureType TypeOfStructure { get; set; }

        public string Dimensions { get; set; }
        public decimal Taxes { get; set; }
        public decimal UserPrice { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
