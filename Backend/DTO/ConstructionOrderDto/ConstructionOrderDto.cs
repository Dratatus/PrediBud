using Backend.Data.Models.Constructions;
using Backend.Data.Models.Constructions.Specyfication;
using Backend.Data.Models.Orders;
using Backend.DTO.Users.Client;
using Backend.DTO.Users.Worker;

namespace Backend.DTO.ConstructionOrderDto
{
    public class ConstructionOrderDto
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public OrderStatus Status { get; set; }
        public ConstructionType ConstructionType { get; set; }
        public string[] PlacementPhotos { get; set; }
        public DateTime? RequestedStartTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? ClientProposedPrice { get; set; }
        public decimal? WorkerProposedPrice { get; set; }
        public decimal? AgreedPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int ClientId { get; set; }
        public ClientDto Client { get; set; }
        public int? WorkerId { get; set; }
        public WorkerDto Worker { get; set; }
        public ConstructionSpecification ConstructionSpecification { get; set; }
        public int ConstructionSpecificationId { get; set; }
    }

}
