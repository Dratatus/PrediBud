using Backend.Data.Models.Constructions;

namespace Backend.DTO.Request
{
    public class CreateOrderRequest
    {
        public string Description { get; set; }
        public ConstructionType ConstructionType { get; set; }
        public object SpecificationDetails { get; set; } 
        public string[] PlacementPhotos { get; set; }
        public DateTime? RequestedStartTime { get; set; }
        public decimal? ClientProposedPrice { get; set; }
        public int ClientId { get; set; }
    }
}
