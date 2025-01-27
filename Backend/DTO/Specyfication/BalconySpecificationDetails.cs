using Backend.Data.Models.Constructions.Dimensions.Balcony;

namespace Backend.DTO.Specyfication
{
    public class BalconySpecificationDetails
    {
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public RailingMaterial? RailingMaterial { get; set; }
    }
}
