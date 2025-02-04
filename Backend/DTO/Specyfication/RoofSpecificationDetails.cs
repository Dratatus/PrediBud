using Backend.Data.Models.Constructions.Specyfication.Roof;

namespace Backend.DTO.Specyfication
{
    public class RoofSpecificationDetails
    {
        public decimal Area { get; set; }
        public decimal Pitch { get; set; }
        public RoofMaterial? Material { get; set; }
    }
}
