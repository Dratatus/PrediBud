using Backend.Data.Consts;
using Backend.Data.Models.Notifications;
using Backend.Data.Models.Orders;
using Backend.Data.Models.Orders.Construction;
using Backend.Data.Models.Users;
using Backend.Middlewares;
using Backend.Repositories;
using Backend.services;
using Backend.Services;
using Moq;

namespace Backend.Tests.Services
{
    public class NegotiationServiceTests
    {
        private readonly Mock<IConstructionOrderRepository> _orderRepositoryMock;
        private readonly Mock<INotificationService> _notificationServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly NegotiationService _service;

        public NegotiationServiceTests()
        {
            _orderRepositoryMock = new Mock<IConstructionOrderRepository>();
            _notificationServiceMock = new Mock<INotificationService>();
            _userRepositoryMock = new Mock<IUserRepository>();

            _service = new NegotiationService(
                _orderRepositoryMock.Object,
                _notificationServiceMock.Object,
                _userRepositoryMock.Object);
        }

        [Fact]
        public async Task InitiateNegotiation_ShouldStartNegotiation_WhenOrderIsValid()
        {
            var orderId = 1;
            var workerId = 2;
            var proposedPrice = 1000m;

            var order = new ConstructionOrder
            {
                ID = orderId,
                Status = OrderStatus.New,
                ClientId = 1
            };

            _orderRepositoryMock.Setup(r => r.GetOrderWithSpecificationByIdAsync(orderId))
                .ReturnsAsync(order);

            var result = await _service.InitiateNegotiation(orderId, workerId, proposedPrice);

            Assert.True(result);
            Assert.Equal(OrderStatus.NegotiationInProgress, order.Status);
            Assert.Equal(workerId, order.WorkerId);
            Assert.Equal(proposedPrice, order.WorkerProposedPrice);

            _orderRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            _notificationServiceMock.Verify(n => n.SendNotificationAsync(It.IsAny<ConstructionOrderNotification>()), Times.Once);
        }

        [Fact]
        public async Task InitiateNegotiation_ShouldThrowException_WhenOrderNotFound()
        {
            var orderId = 1;

            _orderRepositoryMock.Setup(r => r.GetOrderWithSpecificationByIdAsync(orderId))
                .ReturnsAsync((ConstructionOrder)null);

            var exception = await Assert.ThrowsAsync<ApiException>(() => _service.InitiateNegotiation(orderId, 2, 1000m));

            Assert.Equal(ErrorMessages.OrderNotFound, exception.Message);
            Assert.Equal(404, exception.StatusCode);
        }

        [Fact]
        public async Task AcceptNegotiation_ShouldAcceptNegotiation_WhenValid()
        {
            var orderId = 1;
            var clientId = 1;

            var order = new ConstructionOrder
            {
                ID = orderId,
                Status = OrderStatus.NegotiationInProgress,
                ClientId = clientId,
                WorkerId = 2,
                WorkerProposedPrice = 2000m
            };

            var client = new Client { ID = clientId };

            _orderRepositoryMock.Setup(r => r.GetOrderWithSpecificationByIdAsync(orderId))
                .ReturnsAsync(order);

            _userRepositoryMock.Setup(r => r.GetUserByIdAsync(clientId))
                .ReturnsAsync(client);

            var result = await _service.AcceptNegotiation(orderId, clientId);

            Assert.True(result);
            Assert.Equal(OrderStatus.Accepted, order.Status);
            Assert.Equal(order.WorkerProposedPrice, order.AgreedPrice);

            _orderRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            _notificationServiceMock.Verify(n => n.SendNotificationAsync(It.IsAny<ConstructionOrderNotification>()), Times.Once);
        }

        [Fact]
        public async Task RejectNegotiation_ShouldResetOrder_WhenValid()
        {
            var orderId = 1;
            var workerId = 2;

            var order = new ConstructionOrder
            {
                ID = orderId,
                Status = OrderStatus.NegotiationInProgress,
                ClientId = 1,
                WorkerId = workerId,
                WorkerProposedPrice = 2000m,
                BannedWorkerIds = new List<int>()
            };

            var worker = new Worker { ID = workerId, AssignedOrders = new List<ConstructionOrder> { order } };

            _orderRepositoryMock.Setup(r => r.GetOrderWithSpecificationByIdAsync(orderId))
                .ReturnsAsync(order);

            _userRepositoryMock.Setup(r => r.GetUserByIdAsync(workerId))
                .ReturnsAsync(worker);

            var result = await _service.RejectNegotiation(orderId, workerId);

            Assert.True(result);
            Assert.Equal(OrderStatus.New, order.Status);
            Assert.Null(order.WorkerId);
            Assert.Empty(worker.AssignedOrders);
            Assert.Contains(workerId, order.BannedWorkerIds);

            _orderRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            _notificationServiceMock.Verify(n => n.SendNotificationAsync(It.IsAny<ConstructionOrderNotification>()), Times.Once);
        }

        [Fact]
        public async Task CompleteOrder_ShouldMarkOrderAsCompleted_WhenValid()
        {
            var orderId = 1;
            var clientId = 1;

            var order = new ConstructionOrder
            {
                ID = orderId,
                Status = OrderStatus.Accepted,
                ClientId = clientId
            };

            var client = new Client { ID = clientId };

            _orderRepositoryMock.Setup(r => r.GetOrderWithSpecificationByIdAsync(orderId))
                .ReturnsAsync(order);

            _userRepositoryMock.Setup(r => r.GetUserByIdAsync(clientId))
                .ReturnsAsync(client);

            var result = await _service.CompleteOrder(orderId, clientId);

            Assert.True(result);
            Assert.Equal(OrderStatus.Completed, order.Status);

            _orderRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            _notificationServiceMock.Verify(n => n.SendNotificationAsync(It.IsAny<ConstructionOrderNotification>()), Times.Once);
        }
    }
}
