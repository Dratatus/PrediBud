using Backend.Data.Models.Common;
using Backend.Data.Models.Constructions;
using Backend.Data.Models.Constructions.Specyfication;
using Backend.Data.Models.Notifications;
using Backend.Data.Models.Users;

namespace Backend.Data.Models.Orders.Construction
{
    public class ConstructionOrder
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public OrderStatus Status { get; set; }
        public ConstructionType ConstructionType { get; set; }
        public string[] placementPhotos { get; set; }
        public DateTime? RequestedStartTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal? ClientProposedPrice { get; set; }
        public decimal? WorkerProposedPrice { get; set; }
        public decimal? AgreedPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int? WorkerId { get; set; }

        public Worker Worker { get; set; }
        public LastActionBy LastActionBy { get; set; } = LastActionBy.None;
        public int ConstructionSpecificationId { get; set; }
        public ConstructionSpecification ConstructionSpecification { get; set; }

        public List<ConstructionOrderNotification> Notifications { get; set; } = new List<ConstructionOrderNotification>();
        public List<int> BannedWorkerIds { get; set; } = new List<int>();

    }
}
