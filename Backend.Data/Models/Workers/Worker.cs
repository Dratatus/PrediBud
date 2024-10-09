using Backend.Data.Models.Notifications;

namespace Backend.Data.Models.Workers
{
    public class Worker
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public List<ConstructionOrderNotification> Notifications { get; set; } = new List<ConstructionOrderNotification>();
    }
}
