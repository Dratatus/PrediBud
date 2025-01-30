using Backend.Data.Models.Common;
using Backend.Data.Models.Constructions;
using Backend.DTO.Users.PersonalInfo;

namespace Backend.DTO.Request
{
    public class CreateOrderRequest
    {
        public string Description { get; set; }
        public ConstructionType ConstructionType { get; set; }
        public object SpecificationDetails { get; set; } 
        public string[] PlacementPhotos { get; set; }
        public DateOnly? RequestedStartTime { get; set; }
        public decimal? ClientProposedPrice { get; set; }
        public AddressDto Address { get; set; }
        public int ClientId { get; set; }
    }
}
