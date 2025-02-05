using Backend.DTO.ConstructionOrderDto;

namespace Backend.services.Negotiation
{
    public interface INegotiationService
    {
        Task<bool> AcceptNegotiation(int orderId, int userId);
        Task<bool> CompleteOrder(int orderId, int workerId);
        Task<bool> ContinueNegotiation(int orderId, int userId, decimal proposedPrice);
        Task<ConstructionOrderDto> GetClientNegotiationByIdAsync(int orderId, int clientId);
        Task<List<ConstructionOrderDto>> GetClientNegotiationsAsync(int clientId);
        Task<ConstructionOrderDto> GetWorkerNegotiationByIdAsync(int orderId, int workerId);
        Task<List<ConstructionOrderDto>> GetWorkerNegotiationsAsync(int workerId);
        Task<bool> InitiateNegotiation(int orderId, int workerId, decimal proposedPrice);
        Task<bool> RejectNegotiation(int orderId, int rejectingUserId);
    }
}
