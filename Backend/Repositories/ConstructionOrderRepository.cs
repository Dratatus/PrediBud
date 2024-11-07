using Backend.Data.Context;
using Backend.Data.Models.Constructions.Specyfication;
using Backend.Data.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class ConstructionOrderRepository : IConstructionOrderRepository
    {
        private readonly PrediBudDBContext _context;

        public ConstructionOrderRepository(PrediBudDBContext context)
        {
            _context = context;
        }

        public async Task<List<ConstructionOrder>> GetOrdersByClientIdAsync(int clientId)
        {
            return await _context.ConstructionOrders
                .Include(co => co.ConstructionSpecification)
                .Where(co => co.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<ConstructionOrder> GetOrderWithSpecificationByIdAsync(int id)
        {
            return await _context.ConstructionOrders
                .Include(o => o.ConstructionSpecification)
                .FirstOrDefaultAsync(o => o.ID == id);
        }

        public async Task AddAsync(ConstructionOrder order)
        {
            await _context.ConstructionOrders.AddAsync(order);
        }

        public async Task DeleteAsync(ConstructionOrder order)
        {
            _context.ConstructionOrders.Remove(order);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
