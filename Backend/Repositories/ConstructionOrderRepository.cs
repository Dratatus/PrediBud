using Backend.Data.Context;
using Backend.Data.Models.Orders;
using Backend.Data.Models.Orders.Construction;
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

        public async Task<ConstructionOrder> GetClientNegotiationByIdAsync(int orderId, int clientId)
        {
            return await _context.ConstructionOrders
                .Include(o => o.Client)
                .Include(o => o.Worker)
                .Include(o => o.ConstructionSpecification)
                .Include(o => o.Address)
                .FirstOrDefaultAsync(o => o.ID == orderId && o.ClientId == clientId);
        }

        public async Task<ConstructionOrder> GetWorkerNegotiationByIdAsync(int orderId, int workerId)
        {
            return await _context.ConstructionOrders
                .Include(o => o.Client)
                .Include(o => o.Worker)
                .Include(o => o.ConstructionSpecification)
                .Include(o => o.Address)
                .FirstOrDefaultAsync(o => o.ID == orderId && o.WorkerId == workerId);
        }



        public async Task<List<ConstructionOrder>> GetOrdersByClientIdAsync(int clientId)
        {
            return await _context.ConstructionOrders
                .Include(o => o.Address)
                .Include(co => co.Client)
                .ThenInclude(client => client.Address)
                .Include(co => co.Worker)
                .ThenInclude(wokrer => wokrer.Address)
                .Include(co => co.ConstructionSpecification)
                .Where(co => co.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ConstructionOrder>> GetOrdersByWorkerIdAsync(int workerId)
        {
            return await _context.ConstructionOrders
                .Include(o => o.Address)
                .Include(co => co.Client)
                .ThenInclude(client => client.Address)
                .Include(co => co.Worker)
                .ThenInclude(wokrer => wokrer.Address)
                .Include(co => co.ConstructionSpecification)
                .Where(co => co.WorkerId == workerId)
                .ToListAsync();
        }

        public async Task<ConstructionOrder> GetOrderWithSpecificationByIdAsync(int id)
        {
            return await _context.ConstructionOrders
                .Include(o => o.Client)
                .ThenInclude(client => client.Address)
                .Include(o => o.Worker)
                 .ThenInclude(worker => worker.Address)
                .Include(o => o.ConstructionSpecification)
                .Include(o => o.Address)
                .FirstOrDefaultAsync(o => o.ID == id);
        }
        public async Task<IEnumerable<ConstructionOrder>> GetAvailableOrdersAsync(int workerId)
        {
            return await _context.ConstructionOrders
                .Include(o => o.Client) 
                .Include(o => o.ConstructionSpecification)
                .Include(o => o.Address)
                .Where(o => o.Status == OrderStatus.New && !o.BannedWorkerIds.Contains(workerId))
                .ToListAsync();
        }

        public async Task<List<ConstructionOrder>> GetClientNegotiationsAsync(int clientId)
        {
            return await _context.ConstructionOrders
                .Include(o => o.Address)
                .Include(o => o.Client)
                .Include(o => o.Worker)
                .Include(o => o.ConstructionSpecification)
                .Where(o => o.ClientId == clientId && o.Status == OrderStatus.NegotiationInProgress)
                .ToListAsync();
        }

        public async Task<List<ConstructionOrder>> GetWorkerNegotiationsAsync(int workerId)
        {
            return await _context.ConstructionOrders
                .Include(o => o.Address)
                .Include(o => o.Client)
                .Include(o => o.Worker)
                .Include(o => o.ConstructionSpecification)
                .Where(o => o.WorkerId == workerId && o.Status == OrderStatus.NegotiationInProgress)
                .ToListAsync();
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
