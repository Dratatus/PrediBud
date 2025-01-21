using Backend.Data.Models.Constructions.Specyfication;
using Backend.Data.Models.Notifications;
using Backend.Data.Models.Orders;
using Backend.Data.Models.Orders.Construction;
using Backend.DTO.ConstructionOrderDto;
using Backend.DTO.Request;
using Backend.DTO.Users.Client;
using Backend.DTO.Users.Worker;
using Backend.Factories;
using Backend.Repositories;

namespace Backend.services
{
    public class ConstructionOrderService : IConstructionOrderService
    {
        private readonly IConstructionOrderRepository _orderRepository;
        private readonly IConstructionSpecificationFactory _specificationFactory;
        private readonly INotificationService _notificationService;

        public ConstructionOrderService(
            IConstructionOrderRepository orderRepository,
            IConstructionSpecificationFactory specificationFactory,
            INotificationService notificationService)
        {
            _orderRepository = orderRepository;
            _specificationFactory = specificationFactory;
            _notificationService = notificationService;
        }

        public async Task<List<ConstructionOrderDto>> GetOrdersByClientIdAsync(int clientId)
        {
            var orders = await _orderRepository.GetOrdersByClientIdAsync(clientId);

            return orders.Select(order => new ConstructionOrderDto
            {
                ID = order.ID,
                Description = order.Description,
                Status = order.Status,
                ConstructionType = order.ConstructionType,
                PlacementPhotos = order.placementPhotos,
                RequestedStartTime = order.RequestedStartTime,
                StartDate = order.StartDate,
                EndDate = order.EndDate,
                ClientProposedPrice = order.ClientProposedPrice,
                WorkerProposedPrice = order.WorkerProposedPrice,
                AgreedPrice = order.AgreedPrice,
                TotalPrice = order.TotalPrice,
                ClientId = order.ClientId,
                Client = order.Client == null ? null : new ClientDto
                {
                    ID = order.Client.ID,
                    ContactDetails = order.Client.ContactDetails,
                    AddressId = order.Client.AddressId,
                    Address = order.Client.Address
                },
                WorkerId = order.WorkerId,
                Worker = order.Worker == null ? null : new WorkerDto
                {
                    ID = order.Worker.ID,
                    Position = order.Worker.Position,
                    ContactDetails = order.Worker.ContactDetails,
                    AddressId = order.Worker.AddressId,
                    Address = order.Worker.Address
                },
                ConstructionSpecification = order.ConstructionSpecification,
                ConstructionSpecificationId = order.ConstructionSpecificationId
                
            }).ToList();
        }

        public async Task<IEnumerable<ConstructionOrderDto>> GetOrdersByWorkerIdAsync(int workerId)
        {
            var orders = await _orderRepository.GetOrdersByWorkerIdAsync(workerId);

            return orders.Select(order => new ConstructionOrderDto
            {
                ID = order.ID,
                Description = order.Description,
                Status = order.Status,
                ConstructionType = order.ConstructionType,
                PlacementPhotos = order.placementPhotos,
                RequestedStartTime = order.RequestedStartTime,
                StartDate = order.StartDate,
                EndDate = order.EndDate,
                ClientProposedPrice = order.ClientProposedPrice,
                WorkerProposedPrice = order.WorkerProposedPrice,
                AgreedPrice = order.AgreedPrice,
                TotalPrice = order.TotalPrice,
                ClientId = order.ClientId,
                Client = order.Client == null ? null : new ClientDto
                {
                    ID = order.Client.ID,
                    ContactDetails = order.Client.ContactDetails,
                    AddressId = order.Client.AddressId,
                    Address = order.Client.Address
                },
                WorkerId = order.WorkerId,
                Worker = order.Worker == null ? null : new WorkerDto
                {
                    ID = order.Worker.ID,
                    Position = order.Worker.Position,
                    ContactDetails = order.Worker.ContactDetails,
                    AddressId = order.Worker.AddressId,
                    Address = order.Worker.Address
                },
                ConstructionSpecification = order.ConstructionSpecification,
                ConstructionSpecificationId = order.ConstructionSpecificationId
            }).ToList();
        }

        public async Task<IEnumerable<ConstructionOrderDto>> GetAvailableOrdersAsync(int workerId)
        {
            var orders = await _orderRepository.GetAvailableOrdersAsync(workerId);

            return orders.Select(order => new ConstructionOrderDto
            {
                ID = order.ID,
                Description = order.Description,
                Status = order.Status,
                ConstructionType = order.ConstructionType,
                PlacementPhotos = order.placementPhotos,
                RequestedStartTime = order.RequestedStartTime,
                StartDate = order.StartDate,
                EndDate = order.EndDate,
                ClientProposedPrice = order.ClientProposedPrice,
                WorkerProposedPrice = order.WorkerProposedPrice,
                AgreedPrice = order.AgreedPrice,
                TotalPrice = order.TotalPrice,
                ClientId = order.ClientId,
                Client = order.Client == null ? null : new ClientDto
                {
                    ID = order.Client.ID,
                    ContactDetails = order.Client.ContactDetails,
                    AddressId = order.Client.AddressId,
                    Address = order.Client.Address
                },
                ConstructionSpecification = order.ConstructionSpecification,
                ConstructionSpecificationId = order.ConstructionSpecificationId,
            }).ToList();
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

        public async Task<bool> AcceptOrderAsync(int orderId, int workerId)
        {
            var order = await _orderRepository.GetOrderWithSpecificationByIdAsync(orderId);

            if (order == null || order.Status != OrderStatus.New)
                return false;

            order.Status = OrderStatus.Accepted;
            order.WorkerId = workerId;
            await _orderRepository.SaveChangesAsync();

            await _notificationService.SendNotificationAsync(new ConstructionOrderNotification
            {
                ClientId = order.ClientId,
                Title = "Order Accepted",
                Description = "Your order has been accepted by a worker.",
                Status = NotificationStatus.OrderAccepted
            });

            return true;
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
