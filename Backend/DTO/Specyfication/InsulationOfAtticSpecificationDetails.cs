using Backend.Data.Models.Constructions.Specyfication.Insulation;

namespace Backend.DTO.Specyfication
{
    public class InsulationOfAtticSpecificationDetails
    {
        public decimal Area { get; set; }
        public InsulationMaterial Material { get; set; }
        public decimal Thickness { get; set; }
    }
}
