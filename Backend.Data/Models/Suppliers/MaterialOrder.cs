namespace Backend.Data.Models.Suppliers
{
    public class MaterialOrder
    {
        public int ID { get; set; }
        public string Pics { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
