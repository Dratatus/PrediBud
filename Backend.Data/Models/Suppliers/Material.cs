namespace Backend.Data.Models.Suppliers
{
    public class Material
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public decimal PriceWithTaxes { get; set; }
        public decimal PriceWithoutTaxes { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
