using Backend.Data.Models.Constructions.Specyfication.Walls;

namespace Backend.DTO.Specyfication
{
    public class LoadBearingWallSpecificationDetails
    {
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Thickness { get; set; }
        public LoadBearingWallMaterial? Material { get; set; }
    }
}
