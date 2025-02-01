using Backend.Data.Consts;
using Backend.Data.Models.Common;
using Backend.Data.Models.Notifications;
using Backend.Data.Models.Orders;
using Backend.Data.Models.Orders.Construction;
using Backend.Data.Models.Users;
using Backend.DTO.ConstructionOrderDto;
using Backend.DTO.Orders;
using Backend.DTO.Request;
using Backend.DTO.Users.Client;
using Backend.DTO.Users.Worker;
using Backend.Factories;
using Backend.Middlewares;
using Backend.Repositories;
using Backend.services.Notification;
using Backend.Validatiors.Orders.Construction;

namespace Backend.services.Construction
{
    public class ConstructionOrderService : IConstructionOrderService
    {
        private readonly IConstructionOrderRepository _orderRepository;
        private readonly IConstructionSpecificationFactory _specificationFactory;
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;
        public ConstructionOrderService(
            IConstructionOrderRepository orderRepository,
            IConstructionSpecificationFactory specificationFactory,
            INotificationService notificationService, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _specificationFactory = specificationFactory;
            _notificationService = notificationService;
            _userRepository = userRepository;
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
                Client = order.Client == null ? null : new ClientDto
                {
                    ID = order.Client.ID,
                    ContactDetails = order.Client.ContactDetails,
                    AddressId = order.Client.AddressId,
                    Address = order.Client.Address
                },
                Worker = order.Worker == null ? null : new WorkerDto
                {
                    ID = order.Worker.ID,
                    Position = order.Worker.Position,
                    ContactDetails = order.Worker.ContactDetails,
                    AddressId = order.Worker.AddressId,
                    Address = order.Worker.Address
                },
                Address = order.Address != null ? new OrderAddressDto
                {
                    City = order.Address.City,
                    PostCode = order.Address.PostCode,
                    StreetName = order.Address.StreetName
                } : null,
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
                Client = order.Client == null ? null : new ClientDto
                {
                    ID = order.Client.ID,
                    ContactDetails = order.Client.ContactDetails,
                    AddressId = order.Client.AddressId,
                    Address = order.Client.Address
                },
                Worker = order.Worker == null ? null : new WorkerDto
                {
                    ID = order.Worker.ID,
                    Position = order.Worker.Position,
                    ContactDetails = order.Worker.ContactDetails,
                    AddressId = order.Worker.AddressId,
                    Address = order.Worker.Address
                },
                Address = new OrderAddressDto
                {
                    City = order.Address.City,
                    PostCode = order.Address.PostCode,
                    StreetName = order.Address.StreetName
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
                Client = order.Client == null ? null : new ClientDto
                {
                    ID = order.Client.ID,
                    ContactDetails = order.Client.ContactDetails,
                    AddressId = order.Client.AddressId,
                    Address = order.Client.Address
                },
                ConstructionSpecification = order.ConstructionSpecification,
                ConstructionSpecificationId = order.ConstructionSpecificationId,
                Address = new OrderAddressDto
                {
                    City = order.Address.City,
                    PostCode = order.Address.PostCode,
                    StreetName = order.Address.StreetName
                }
            }).ToList();
        }
        public async Task<ConstructionOrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderWithSpecificationByIdAsync(id);

            if (order == null)
            {
                throw new ApiException(ErrorMessages.OrderNotFound, StatusCodes.Status404NotFound);
            }

            var orderDTo = new ConstructionOrderDto
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
                Worker = order.Worker == null ? null : new WorkerDto
                {
                    ID = order.Worker.ID,
                    ContactDetails = order.Worker.ContactDetails,
                    AddressId = order.Worker.AddressId,
                    Address = order.Worker.Address
                },
                Client = order.Client == null ? null : new ClientDto
                {
                    ID = order.Client.ID,
                    ContactDetails = order.Client.ContactDetails,
                    AddressId = order.Client.AddressId,
                    Address = order.Client.Address
                },   
                ConstructionSpecification = order.ConstructionSpecification,
                ConstructionSpecificationId = order.ConstructionSpecificationId,
                Address = new OrderAddressDto
                {
                    City = order.Address.City,
                    PostCode = order.Address.PostCode,
                    StreetName = order.Address.StreetName
                }
            };


            return orderDTo;
        }

        public async Task<ConstructionOrderDto> CreateOrderAsync(CreateOrderRequest request)
        {
            CreateOrderRequestValidator.Validate(request);

            var specification = _specificationFactory.CreateSpecification(request.ConstructionType, request.SpecificationDetails);
            var user = await _userRepository.GetUserByIdAsync(request.ClientId);

            var isClient = user is Client;

            if (user is not Client)
            {
                throw new ApiException(ErrorMessages.OnlyClientCanCreateOrder, StatusCodes.Status403Forbidden);
            }

            var order = new ConstructionOrder
            {
                Description = request.Description,
                ConstructionType = request.ConstructionType,
                placementPhotos = request.PlacementPhotos,
                RequestedStartTime = request.RequestedStartTime,
                ClientProposedPrice = request.ClientProposedPrice,
                ClientId = request.ClientId,
                ConstructionSpecification = specification,
                Address = new OrderAddress { City = request.Address.City, PostCode = request.Address.PostCode, StreetName = request.Address.StreetName }
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return new ConstructionOrderDto
            {
                ID = order.ID,
                Description = order.Description,
                ConstructionType = order.ConstructionType,
                RequestedStartTime = order.RequestedStartTime,
                ClientProposedPrice = order.ClientProposedPrice,
                PlacementPhotos = order.placementPhotos,
                ConstructionSpecification = specification,
                Client = new ClientDto
                {
                    ID = order.ClientId
                },
                Address = new OrderAddressDto
                {
                    City = order.Address.City,
                    PostCode = order.Address.PostCode,
                    StreetName = order.Address.StreetName
                }
            };
        }

        public async Task<bool> AcceptOrderAsync(int orderId, int workerId)
        {
            var order = await _orderRepository.GetOrderWithSpecificationByIdAsync(orderId);

            if (order == null || order.Status != OrderStatus.New) throw new ApiException(ErrorMessages.InvalidOrder, StatusCodes.Status400BadRequest);

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

            if (order == null)
            {
                throw new ApiException(ErrorMessages.OrderNotFound, StatusCodes.Status404NotFound);
            }

            if (order.ClientId != clientId)
            {
                throw new ApiException(ErrorMessages.UnauthorizedOrderAccess, StatusCodes.Status403Forbidden);
            }

            await _orderRepository.DeleteAsync(order);
            await _orderRepository.SaveChangesAsync();
            return true;
        }
    }
}
