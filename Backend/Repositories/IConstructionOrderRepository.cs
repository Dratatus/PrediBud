using Backend.Data.Models.Constructions.Specyfication;
using Backend.Data.Models.Orders.Construction;
using Backend.DTO.ConstructionOrderDto;

namespace Backend.Repositories
{
    public interface IConstructionOrderRepository
    {
        Task<ConstructionOrder> GetOrderWithSpecificationByIdAsync(int id);
        Task<List<ConstructionOrder>> GetOrdersByClientIdAsync(int clientId);
        Task<IEnumerable<ConstructionOrder>> GetOrdersByWorkerIdAsync(int workerId);
        Task<IEnumerable<ConstructionOrder>> GetAvailableOrdersAsync(int workerId);
        Task AddAsync(ConstructionOrder order);
        Task DeleteAsync(ConstructionOrder order);
        Task SaveChangesAsync();
        Task<List<ConstructionOrder>> GetWorkerNegotiationsAsync(int workerId);
        Task<List<ConstructionOrder>> GetClientNegotiationsAsync(int clientId);
        Task<ConstructionOrder> GetClientNegotiationByIdAsync(int orderId, int clientId);
        Task<ConstructionOrder> GetWorkerNegotiationByIdAsync(int orderId, int workerId);
    }
}
