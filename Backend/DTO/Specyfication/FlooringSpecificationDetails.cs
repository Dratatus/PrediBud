using Backend.Data.Models.Constructions.Dimensions.Floor;

namespace Backend.DTO.Specyfication
{
    public class FlooringSpecificationDetails
    {
        public decimal Area { get; set; }
        public FlooringMaterial Material { get; set; }
    }
}
