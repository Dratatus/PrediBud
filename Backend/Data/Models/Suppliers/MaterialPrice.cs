namespace Backend.Data.Models.Suppliers
{
    public class MaterialPrice
    {
        public int ID { get; set; }
        public string MaterialType { get; set; } 
        public string MaterialCategory { get; set; } 
        public decimal PricePerUnit { get; set; } 

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } 
    }
}
