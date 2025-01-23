using Backend.Data.Context;
using Backend.Data.Models.Suppliers;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class MaterialPriceRepository : IMaterialPriceRepository
    {
        private readonly PrediBudDBContext _context;

        public MaterialPriceRepository(PrediBudDBContext context)
        {
            _context = context;
        }

        public async Task<MaterialPrice> GetMaterialPriceByIdAsync(int id)
        {
            return await _context.MaterialPrices
                .Include(mp => mp.Supplier)
               .FirstOrDefaultAsync(mp => mp.ID == id);
        }

        public async Task<IEnumerable<MaterialPrice>> GetAllMaterialPricesAsync()
        {
            return await _context.MaterialPrices.Include(mp => mp.Supplier).ToListAsync();
        }

        public async Task AddMaterialPriceAsync(MaterialPrice materialPrice)
        {
            await _context.MaterialPrices.AddAsync(materialPrice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMaterialPriceAsync(MaterialPrice materialPrice)
        {
            _context.MaterialPrices.Update(materialPrice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMaterialPriceAsync(int id)
        {
            var existing = await GetMaterialPriceByIdAsync(id);
            if (existing != null)
            {
                _context.MaterialPrices.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
