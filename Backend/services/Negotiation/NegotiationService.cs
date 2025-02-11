﻿using Backend.Data.Models.Orders;
using Backend.Data.Models.Notifications;
using Backend.Repositories;
using Backend.Data.Models.Users;
using Backend.Data.Consts;
using Backend.Middlewares;
using Backend.Validatiors.Negotiation;
using Backend.services.Notification;
using Backend.DTO.ConstructionOrderDto;
using Backend.services.Negotiation.Mapper;

namespace Backend.services.Negotiation
{
    public class NegotiationService : INegotiationService
    {
        private readonly IConstructionOrderRepository _orderRepository;
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;
        private readonly INegotiationMapper _negotiationMapper;


        public NegotiationService(IConstructionOrderRepository orderRepository, INotificationService notificationService, IUserRepository userRepository, INegotiationMapper negotiationMapper)
        {
            _orderRepository = orderRepository;
            _notificationService = notificationService;
            _userRepository = userRepository;
            _negotiationMapper = negotiationMapper;
        }

        public async Task<List<ConstructionOrderDto>> GetClientNegotiationsAsync(int clientId)
        {
            var orders = await _orderRepository.GetClientNegotiationsAsync(clientId);
            return _negotiationMapper.MapToDtoList(orders);
        }

        public async Task<List<ConstructionOrderDto>> GetWorkerNegotiationsAsync(int workerId)
        {
            var orders = await _orderRepository.GetWorkerNegotiationsAsync(workerId);
            return _negotiationMapper.MapToDtoList(orders);
        }

        public async Task<ConstructionOrderDto> GetClientNegotiationByIdAsync(int orderId, int clientId)
        {
            ClientNegotiationValidator.Validate(orderId, clientId);

            var order = await _orderRepository.GetClientNegotiationByIdAsync(orderId, clientId);

            if (order == null)
            {
                throw new ApiException(ErrorMessages.NegotiationNotFound, StatusCodes.Status404NotFound);
            }

            return _negotiationMapper.MapToDto(order);
        }

        public async Task<ConstructionOrderDto> GetWorkerNegotiationByIdAsync(int orderId, int workerId)
        {
            WorkerNegotiationValidator.Validate(orderId, workerId);

            var order = await _orderRepository.GetWorkerNegotiationByIdAsync(orderId, workerId);

            if (order == null)
            {
                throw new ApiException(ErrorMessages.NegotiationNotFound, StatusCodes.Status404NotFound);
            }

            return _negotiationMapper.MapToDto(order);
        }


        public async Task<bool> InitiateNegotiation(int orderId, int workerId, decimal proposedPrice)
        {
            var order = await _orderRepository.GetOrderWithSpecificationByIdAsync(orderId);

            if (order == null)
            {
                throw new ApiException(ErrorMessages.OrderNotFound, StatusCodes.Status404NotFound);
            }

            if (order.Status != OrderStatus.New)
            {
                throw new ApiException(ErrorMessages.OrderNotNew, StatusCodes.Status400BadRequest);
            }

            InitiateNegotiationValidator.Validate(workerId, proposedPrice);

            order.Status = OrderStatus.NegotiationInProgress;
            order.WorkerId = workerId;
            order.WorkerProposedPrice = proposedPrice;
            order.LastActionBy = LastActionBy.Worker;

            await _orderRepository.SaveChangesAsync();

            await _notificationService.SendNotificationAsync(new ConstructionOrderNotification
            {
                ClientId = order.ClientId,
                Title = "Negocjacja rozpoczęta",
                Description = $"Pracownik zaproponował cenę w wysokości: {proposedPrice}.",
                Status = NotificationStatus.NegotiationStarted,
                ConstructionOrderID = order.ID,
                Date = DateTime.Now
            });

            return true;
        }

        public async Task<bool> AcceptNegotiation(int orderId, int userId)
        {
            var order = await _orderRepository.GetOrderWithSpecificationByIdAsync(orderId);

            if (order == null)
            {
                throw new ApiException(ErrorMessages.OrderNotFound, StatusCodes.Status404NotFound);
            }

            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new ApiException(ErrorMessages.UserNotFound, StatusCodes.Status404NotFound);
            }

            if (order.Status != OrderStatus.NegotiationInProgress)
            {
                throw new ApiException(ErrorMessages.OrderNotInNegotiation, StatusCodes.Status400BadRequest);
            }

            AcceptNegotiationValidator.Validate(user);

            var isClient = user is Client && order.ClientId == userId;
            var isWorker = user is Worker && order.WorkerId == userId;

            if (!isClient && !isWorker)
            {
                throw new ApiException(ErrorMessages.UnauthorizedAccess, StatusCodes.Status403Forbidden);
            }

            if (isClient) order.LastActionBy = LastActionBy.Client;
            else if (isWorker) order.LastActionBy = LastActionBy.Worker;

            order.Status = OrderStatus.Accepted;
            order.AgreedPrice = isClient ? order.WorkerProposedPrice : order.ClientProposedPrice;
            order.StartDate = order.RequestedStartTime;

            if (isWorker && user is Worker worker)
            {
                worker.AssignedOrders.Add(order);
            }

            await _orderRepository.SaveChangesAsync();
            var notification = new ConstructionOrderNotification
            {
                Title = "Negocjacja zaakceptowana",
                Description = isClient ? "Klient zaakceptował zaproponowane warunki." : "Pracownik zaakceptował zaproponowane warunki.",
                Status = NotificationStatus.OrderAccepted,
                WorkerId = isClient ? order.WorkerId.GetValueOrDefault() : null,
                ClientId = isClient ? null : order.ClientId,
                ConstructionOrderID = order.ID,
                Date = DateTime.Now
            };

            await _notificationService.SendNotificationAsync(notification);

            return true;
        }

        public async Task<bool> RejectNegotiation(int orderId, int rejectingUserId)
        {
            var order = await _orderRepository.GetOrderWithSpecificationByIdAsync(orderId);
            if (order == null)
            {
                throw new ApiException(ErrorMessages.OrderNotFound, StatusCodes.Status404NotFound);
            }

            var user = await _userRepository.GetUserByIdAsync(rejectingUserId);
            if (user == null)
            {
                throw new ApiException(ErrorMessages.UserNotFound, StatusCodes.Status404NotFound);
            }

            var isClient = user is Client && order.ClientId == rejectingUserId;
            var isWorker = user is Worker && order.WorkerId == rejectingUserId;

            if (!isClient && !isWorker)
            {
                throw new ApiException(ErrorMessages.UnauthorizedAccess, StatusCodes.Status403Forbidden);
            }

            if (isClient && order.LastActionBy == LastActionBy.Client ||
                isWorker && order.LastActionBy == LastActionBy.Worker)
            {
                throw new ApiException(ErrorMessages.InvalidNegotiationAction, StatusCodes.Status400BadRequest);
            }

            order.BannedWorkerIds.Add(order.WorkerId.Value);
            order.Status = OrderStatus.New;

            int bannedWorker = order.WorkerId.Value;
            int bannedClient = order.ClientId;

            order.WorkerId = null;
            order.WorkerProposedPrice = null;

            var worker = await _userRepository.GetUserByIdAsync(bannedWorker) as Worker;

            if (worker != null)
            {
                var assignedOrder = worker.AssignedOrders.FirstOrDefault(o => o.ID == orderId);
                if (assignedOrder != null)
                {
                    worker.AssignedOrders.Remove(assignedOrder);
                }
            }

            await _orderRepository.SaveChangesAsync();

            await _notificationService.SendNotificationAsync(new ConstructionOrderNotification
            {
                ClientId = isClient ? null : bannedClient,
                WorkerId = isClient ? bannedWorker : null,
                Title = "Negocjacje odrzucone",
                Description = isClient ? "Klient odrzucił warunki." : "Pracownik odrzucił warunki.",
                Status = NotificationStatus.NegotiationRejected,
                ConstructionOrderID = order.ID,
                Date = DateTime.Now
            });

            return true;
        }


        public async Task<bool> ContinueNegotiation(int orderId, int userId, decimal proposedPrice)
        {
            var order = await _orderRepository.GetOrderWithSpecificationByIdAsync(orderId);
            if (order == null)
            {
                throw new ApiException(ErrorMessages.OrderNotFound, StatusCodes.Status404NotFound);
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new ApiException(ErrorMessages.UserNotFound, StatusCodes.Status404NotFound);
            }

            if (order.Status != OrderStatus.NegotiationInProgress)
            {
                throw new ApiException(ErrorMessages.OrderNotInNegotiation, StatusCodes.Status400BadRequest);
            }

            var isClient = user is Client && order.ClientId == userId;
            var isWorker = user is Worker && order.WorkerId == userId;

            if (isClient && order.LastActionBy == LastActionBy.Client ||
                isWorker && order.LastActionBy == LastActionBy.Worker)
            {
                throw new ApiException(ErrorMessages.InvalidNegotiationAction, StatusCodes.Status400BadRequest);
            }

            if (isClient)
            {
                order.ClientProposedPrice = proposedPrice;
            }
            else if (isWorker)
            {
                order.WorkerProposedPrice = proposedPrice;
            }
            else
            {
                throw new ApiException(ErrorMessages.UnauthorizedAccess, StatusCodes.Status403Forbidden);
            }

            order.LastActionBy = isClient ? LastActionBy.Client : LastActionBy.Worker;

            await _orderRepository.SaveChangesAsync();

            await _notificationService.SendNotificationAsync(new ConstructionOrderNotification
            {
                ClientId = isWorker ? order.ClientId : null,
                WorkerId = isClient ? order.WorkerId.GetValueOrDefault() : null,
                Title = "Kontynuacja negocjacji",
                Description = isClient ? "Klient kontynuuje negocjacje." : "Pracownik kontynuuje negocjacje",
                Status = NotificationStatus.ContinuedNegotiation,
                ConstructionOrderID = order.ID,
                Date = DateTime.Now
            });

            return true;
        }

        public async Task<bool> CompleteOrder(int orderId, int userId)
        {
            var order = await _orderRepository.GetOrderWithSpecificationByIdAsync(orderId);
            if (order == null)
            {
                throw new ApiException(ErrorMessages.OrderNotFound, StatusCodes.Status404NotFound);
            }

            if (order.Status != OrderStatus.Accepted)
            {
                throw new ApiException(ErrorMessages.OrderNotAccepted, StatusCodes.Status400BadRequest);
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new ApiException(ErrorMessages.UserNotFound, StatusCodes.Status404NotFound);
            }

            var isClient = user is Client && order.ClientId == userId;
            var isWorker = user is Worker && order.WorkerId == userId;

            if (!isClient && !isWorker)
            {
                throw new ApiException(ErrorMessages.UnauthorizedAccess, StatusCodes.Status403Forbidden);
            }

            order.LastActionBy = isClient ? LastActionBy.Client : LastActionBy.Worker;
            order.EndDate = DateOnly.FromDateTime(DateTime.Now);
            order.Status = OrderStatus.Completed;

            await _orderRepository.SaveChangesAsync();

            await _notificationService.SendNotificationAsync(new ConstructionOrderNotification
            {
                ClientId = isWorker ? order.ClientId : null,
                WorkerId = isClient ? order.WorkerId.GetValueOrDefault() : null,
                Title = "Zamówienie zrealizowane",
                Description = isClient
                    ? "Klient oznaczył zamówienie jako zrealizowane."
                    : "Pracownik oznaczył zamówienie jako zakończone.",
                Status = NotificationStatus.OrderCompleted,
                ConstructionOrderID = order.ID,
                Date = DateTime.Now
            });

            return true;
        }
    }
}