using Backend.Data.Context;
using Backend.Data.Models.Orders.Material;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class MaterialOrderRepository : IMaterialOrderRepository
    {
        private readonly PrediBudDBContext _context;

        public MaterialOrderRepository(PrediBudDBContext context)
        {
            _context = context;
        }

        public async Task<MaterialOrder> GetMaterialOrderByIdAsync(int orderId)
        {
            return await _context.MaterialOrders
                .Include(mo => mo.MaterialPrice)
                .Include(mo => mo.Supplier)
                .ThenInclude(suplier => suplier.Address)
                .Include(mo => mo.Supplier.Address)
                .Include(mo => mo.Address)
                .FirstOrDefaultAsync(mo => mo.ID == orderId);
        }


        public async Task<IEnumerable<MaterialOrder>> GetAllMaterialOrdersAsync()
        {
            return await _context.MaterialOrders
                .Include(mo => mo.MaterialPrice)
                .Include(mo => mo.Supplier)
                .Include(mo => mo.Supplier.Address)
                .Include(mo => mo.Address)
                .ToListAsync();
        }

        public async Task AddMaterialOrderAsync(MaterialOrder materialOrder)
        {
            await _context.MaterialOrders.AddAsync(materialOrder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMaterialOrderAsync(MaterialOrder materialOrder)
        {
            _context.MaterialOrders.Update(materialOrder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMaterialOrderAsync(int orderId)
        {
            var order = await GetMaterialOrderByIdAsync(orderId);
            _context.MaterialOrders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
