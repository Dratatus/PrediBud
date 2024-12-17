using Backend.Data.Models.Suppliers;
using Backend.Data.Models.Users;

namespace Backend.Data.Models.Orders.Material
{
    public class MaterialOrder
    {
        public int ID { get; set; }
        public string MaterialType { get; set; } // Typ materiału, np. "Plaster"
        public string MaterialCategory { get; set; } // Kategoria materiału, np. "FinishMaterial"
        public decimal UnitPrice { get; set; } // Cena jednostkowa
        public int Quantity { get; set; } // Ilość zamówionego materiału
        public decimal? Taxes { get; set; } // Opcjonalne podatki (np. 0.23 dla 23% VAT)

        public decimal TotalPrice => UnitPrice * Quantity; // Łączna cena bez podatków
        public decimal TotalPriceWithTaxes => Taxes.HasValue
            ? TotalPrice * (1 + Taxes.Value)
            : TotalPrice; // Łączna cena z podatkami, jeśli są podane

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Data zamówienia

        // Powiązanie z użytkownikiem (Worker lub Client)
        public int UserId { get; set; }
        public User User { get; set; }

        // Powiązanie z dostawcą
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public List<MaterialPrice> MaterialPrices { get; set; } = new List<MaterialPrice>();
    }
}
