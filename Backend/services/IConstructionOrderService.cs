using Backend.Data.Models.Orders;
using Backend.DTO.Request;

namespace Backend.services
{
    public interface IConstructionOrderService
    {
        Task<ConstructionOrder> CreateOrderAsync(CreateOrderRequest request);
        Task<ConstructionOrder> GetOrderByIdAsync(int id);
        Task<List<ConstructionOrder>> GetOrdersByClientIdAsync(int clientId);
        Task<bool> DeleteOrderAsync(int clientId, int orderId);
        Task<bool> AcceptOrderAsync(int orderId, int workerId);
    }
}
