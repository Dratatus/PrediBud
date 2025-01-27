using Backend.Data.Models.Constructions.Specyfication.Ceiling;

namespace Backend.DTO.Specyfication
{
    public class SuspendedCeilingSpecificationDetails
    {
        public decimal Area { get; set; }
        public decimal Height { get; set; }
        public SuspendedCeilingMaterial? Material { get; set; }
    }
}
