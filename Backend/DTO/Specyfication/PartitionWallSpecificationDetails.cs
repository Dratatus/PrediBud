using Backend.Data.Models.Constructions.Specyfication.Walls;

namespace Backend.DTO.Specyfication
{
    namespace Backend.DTO.Specifications
    {
        public class PartitionWallSpecificationDetails
        {
            public decimal Height { get; set; }
            public decimal Width { get; set; }
            public decimal Thickness { get; set; }
            public PartitionWallMaterial? Material { get; set; }
        }
    }
}
