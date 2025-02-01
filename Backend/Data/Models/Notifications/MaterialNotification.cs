using Backend.Data.Models.Suppliers;

namespace Backend.Data.Models.Notifications
{
    public class MaterialNotification
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public Supplier Supplier { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
