using Backend.Data.Models.Constructions.Specyfication.Stairs;

namespace Backend.DTO.Specyfication
{
    public class StaircaseSpecificationDetails
    {
        public int NumberOfSteps { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public StaircaseMaterial Material { get; set; }
    }
}
