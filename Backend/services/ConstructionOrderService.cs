using Backend.Data.Models.Orders;
using Backend.DTO.Request;
using Backend.Factories;
using Backend.Repositories;

namespace Backend.services
{
    public class ConstructionOrderService : IConstructionOrderService
    {
        private readonly IConstructionOrderRepository _orderRepository;
        private readonly IConstructionSpecificationFactory _specificationFactory;

        public ConstructionOrderService(
            IConstructionOrderRepository orderRepository,
            IConstructionSpecificationFactory specificationFactory)
        {
            _orderRepository = orderRepository;
            _specificationFactory = specificationFactory;
        }

        public async Task<List<ConstructionOrder>> GetOrdersByClientIdAsync(int clientId)
        {
            return await _orderRepository.GetOrdersByClientIdAsync(clientId);
        }

        public async Task<ConstructionOrder> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetOrderWithSpecificationByIdAsync(id);
        }

        public async Task<ConstructionOrder> CreateOrderAsync(CreateOrderRequest request)
        {
            var specification = _specificationFactory.CreateSpecification(request.ConstructionType, request.SpecificationDetails);

            var order = new ConstructionOrder
            {
                Description = request.Description,
                ConstructionType = request.ConstructionType,
                placementPhotos = request.PlacementPhotos,
                RequestedStartTime = request.RequestedStartTime,
                ClientProposedPrice = request.ClientProposedPrice,
                ClientId = request.ClientId,
                ConstructionSpecification = specification
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task<bool> DeleteOrderAsync(int clientId, int orderId)
        {
            var order = await _orderRepository.GetOrderWithSpecificationByIdAsync(orderId);

            if (order == null || order.ClientId != clientId)
            {
                return false;
            }

            await _orderRepository.DeleteAsync(order);
            await _orderRepository.SaveChangesAsync();
            return true;
        }
    }
}
