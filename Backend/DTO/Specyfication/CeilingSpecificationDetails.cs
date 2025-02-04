using Backend.Data.Models.Constructions.Specyfication.Ceiling;

namespace Backend.DTO.Specyfication
{
    public class CeilingSpecificationDetails
    {
        public decimal Area { get; set; }
        public CeilingMaterial? Material { get; set; }
    }
}
