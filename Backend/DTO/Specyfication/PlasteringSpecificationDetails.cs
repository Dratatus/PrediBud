using Backend.Data.Models.Constructions.Specyfication.Plastering;

namespace Backend.DTO.Specyfication
{
    public class PlasteringSpecificationDetails
    {
        public decimal WallSurfaceArea { get; set; }
        public PlasterType? PlasterType { get; set; }
    }
}
