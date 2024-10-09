namespace Backend.Data.Models.Suppliers
{
    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Material> AvailableMaterials { get; set; } = new List<Material>();
    }
}
