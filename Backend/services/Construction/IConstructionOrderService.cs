using Backend.Data.Models.Orders.Construction;
using Backend.DTO.ConstructionOrderDto;
using Backend.DTO.Request;

namespace Backend.services.Construction
{
    public interface IConstructionOrderService
    {
        Task<ConstructionOrderDto> CreateOrderAsync(CreateOrderRequest request);
        Task<ConstructionOrderDto> GetOrderByIdAsync(int id);
        Task<bool> DeleteOrderAsync(int clientId, int orderId);
        Task<bool> AcceptOrderAsync(int orderId, int workerId);
        Task<List<ConstructionOrderDto>> GetOrdersByClientIdAsync(int clientId);
        Task<IEnumerable<ConstructionOrderDto>> GetOrdersByWorkerIdAsync(int workerId);
        Task<IEnumerable<ConstructionOrderDto>> GetAvailableOrdersAsync(int workerId);
    }
}
