using Backend.Data.Models.Orders;
using Backend.Data.Models.Notifications;
using Backend.Repositories;
using Backend.services;
using Backend.Data.Models.Users;

namespace Backend.Services
{
    public class NegotiationService : INegotiationService
    {
        private readonly IConstructionOrderRepository _orderRepository;
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;

        public NegotiationService(IConstructionOrderRepository orderRepository, INotificationService notificationService, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _notificationService = notificationService;
            _userRepository = userRepository;
        }

        public async Task<bool> InitiateNegotiation(int orderId, int workerId, decimal proposedPrice)
        {
            var order = await _orderRepository.GetOrderWithSpecificationByIdAsync(orderId);

            if (order == null || order.Status != OrderStatus.New)
                return false;

            order.Status = OrderStatus.NegotiationInProgress;
            order.WorkerId = workerId;
            order.WorkerProposedPrice = proposedPrice;

            await _orderRepository.SaveChangesAsync();

            await _notificationService.SendNotificationAsync(new ConstructionOrderNotification
            {
                ClientId = order.ClientId,
                Title = "Negotiation Started",
                Description = $"Worker has proposed a price of {proposedPrice}.",
                Status = NotificationStatus.NegotiationStarted, 
                ConstructionOrderID = order.ID
            });

            return true;
        }

        public async Task<bool> AcceptNegotiation(int orderId, int userId)
        {
            var order = await _orderRepository.GetOrderWithSpecificationByIdAsync(orderId);
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (order == null || user == null || order.Status != OrderStatus.NegotiationInProgress)
                return false;

            var isClient = user is Client && order.ClientId == userId;
            var isWorker = user is Worker && order.WorkerId == userId;

            if (!isClient && !isWorker)
                return false;


            order.Status = OrderStatus.Accepted;

            if (isWorker && user is Worker worker)
            {
                worker.AssignedOrders.Add(order);
            }

            await _orderRepository.SaveChangesAsync();
            var notification = new ConstructionOrderNotification
            {
                Title = "Negotiation Accepted",
                Description = isClient ? "Client has accepted the proposed terms." : "Worker has accepted the proposed terms.",
                Status = NotificationStatus.OrderAccepted,
                WorkerId = isClient ? order.WorkerId.GetValueOrDefault() : 0,
                ClientId = isClient ? null : order.ClientId,
                ConstructionOrderID = order.ID
            };

            await _notificationService.SendNotificationAsync(notification);

            return true;
        }

        public async Task<bool> RejectNegotiation(int orderId, int rejectingUserId, bool isClient)
        {
            var order = await _orderRepository.GetOrderWithSpecificationByIdAsync(orderId);

            if (order == null || (isClient && order.ClientId != rejectingUserId) || (!isClient && order.WorkerId != rejectingUserId))
                return false;

            order.BannedWorkerIds.Add(order.WorkerId.Value);
            order.Status = OrderStatus.New;

            int bannedWordker = order.WorkerId.Value;
            int bannedClient = order.ClientId;

            order.WorkerId = null;
            order.WorkerProposedPrice = null;

            var worker = await _userRepository.GetUserByIdAsync(bannedWordker) as Worker;

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
                WorkerId = isClient ? bannedWordker : null,
                Title = "Negotiation Rejected",
                Description = isClient ? "Client has rejected the terms." : "Worker has rejected the terms.",
                Status = NotificationStatus.NegotiationRejected,
                ConstructionOrderID = order.ID
            });

            return true;
        }

        public async Task<bool> ContinueNegotiation(int orderId, int userId, bool isClient, decimal proposedPrice)
        {
            var order = await _orderRepository.GetOrderWithSpecificationByIdAsync(orderId);

            if (order == null || order.Status != OrderStatus.NegotiationInProgress)
                return false;

            if (isClient && order.ClientId == userId)
                order.ClientProposedPrice = proposedPrice;
            else if (!isClient && order.WorkerId == userId)
                order.WorkerProposedPrice = proposedPrice;
            else
                return false;

            await _orderRepository.SaveChangesAsync();

            await _notificationService.SendNotificationAsync(new ConstructionOrderNotification
            {
                ClientId = isClient ? 0 : order.ClientId,
                WorkerId = isClient ? order.WorkerId.GetValueOrDefault() : 0,
                Title = "Continued Negotiation",
                Description = isClient ? "Client has continued negotiations." : "Worker has continued negotiations.",
                Status = NotificationStatus.ContinuedNegotiation,
                ConstructionOrderID = order.ID
            });

            return true;
        }

        public async Task<bool> CompleteOrder(int orderId, int workerId)
        {
            var order = await _orderRepository.GetOrderWithSpecificationByIdAsync(orderId);

            if (order == null || order.WorkerId != workerId || order.Status != OrderStatus.Accepted)
                return false;

            order.Status = OrderStatus.Completed;
            await _orderRepository.SaveChangesAsync();

            await _notificationService.SendNotificationAsync(new ConstructionOrderNotification
            {
                ClientId = order.ClientId,
                Title = "Order Completed",
                Description = "The worker has marked the order as completed.",
                Status = NotificationStatus.OrderCompleted
            });

            return true;
        }
    }
}
