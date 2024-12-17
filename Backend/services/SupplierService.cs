using Backend.Data.Context;
using Backend.Data.Models.Suppliers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;

namespace Backend.services
{
    public class SupplierService : ISupplierService
    {
        private readonly PrediBudDBContext _context;

        public SupplierService(PrediBudDBContext context)
        {
            _context = context;
        }

        public async Task UpdateSuppliersAsync(string jsonFilePath)
        {
            try
            {
                var json = await File.ReadAllTextAsync(jsonFilePath);
                var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(json);

                if (suppliers == null || !suppliers.Any())
                {
                    Console.WriteLine("Nie znaleziono żadnych dostawców w pliku JSON.");
                    return;
                }

                foreach (var supplier in suppliers)
                {
                    var existingSupplier = await _context.Suppliers
                        .Include(s => s.MaterialPrices) // Pobierz także powiązane ceny materiałów
                        .FirstOrDefaultAsync(s => s.Name == supplier.Name);

                    if (existingSupplier != null)
                    {
                        // Aktualizacja istniejącego dostawcy
                        existingSupplier.Address = supplier.Address;
                        existingSupplier.ContactEmail = supplier.ContactEmail;

                        // Aktualizacja lub dodanie cen materiałów
                        foreach (var materialPrice in supplier.MaterialPrices)
                        {
                            var existingMaterialPrice = existingSupplier.MaterialPrices
                                .FirstOrDefault(mp => mp.MaterialType == materialPrice.MaterialType &&
                                                      mp.MaterialCategory == materialPrice.MaterialCategory);

                            if (existingMaterialPrice != null)
                            {
                                // Aktualizacja istniejącej ceny
                                existingMaterialPrice.PricePerUnit = materialPrice.PricePerUnit;
                            }
                            else
                            {
                                // Dodanie nowej ceny materiału
                                existingSupplier.MaterialPrices.Add(new MaterialPrice
                                {
                                    MaterialType = materialPrice.MaterialType,
                                    MaterialCategory = materialPrice.MaterialCategory,
                                    PricePerUnit = materialPrice.PricePerUnit,
                                    SupplierId = existingSupplier.ID
                                });
                            }
                        }
                    }
                    else
                    {
                        // Dodanie nowego dostawcy wraz z cenami materiałów
                        await _context.Suppliers.AddAsync(new Supplier
                        {
                            Name = supplier.Name,
                            Address = supplier.Address,
                            ContactEmail = supplier.ContactEmail,
                            MaterialPrices = supplier.MaterialPrices.Select(mp => new MaterialPrice
                            {
                                MaterialType = mp.MaterialType,
                                MaterialCategory = mp.MaterialCategory,
                                PricePerUnit = mp.PricePerUnit
                            }).ToList()
                        });
                    }
                }

                await _context.SaveChangesAsync();
                Console.WriteLine("Dane dostawców zostały zaktualizowane.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas aktualizacji dostawców: {ex.Message}");
            }
        }
    }
}
