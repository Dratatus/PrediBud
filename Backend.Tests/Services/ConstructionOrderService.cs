using Backend.Data.Consts;
using Backend.Data.Models.Common;
using Backend.Data.Models.Constructions;
using Backend.Data.Models.Constructions.Specyfication.Foundation;
using Backend.Data.Models.Notifications;
using Backend.Data.Models.Orders;
using Backend.Data.Models.Orders.Construction;
using Backend.Data.Models.Users;
using Backend.DTO.Request;
using Backend.DTO.Users.PersonalInfo;
using Backend.Factories;
using Backend.Middlewares;
using Backend.Repositories;
using Backend.services.Construction;
using Backend.services.Notification;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Backend.Tests.Services
{
    public class ConstructionOrderServiceTests
    {
        private readonly Mock<IConstructionOrderRepository> _orderRepositoryMock;
        private readonly Mock<IConstructionSpecificationFactory> _specificationFactoryMock;
        private readonly Mock<INotificationService> _notificationServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly ConstructionOrderService _service;

        public ConstructionOrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IConstructionOrderRepository>();
            _specificationFactoryMock = new Mock<IConstructionSpecificationFactory>();
            _notificationServiceMock = new Mock<INotificationService>();
            _userRepositoryMock = new Mock<IUserRepository>();

            _service = new ConstructionOrderService(
                _orderRepositoryMock.Object,
                _specificationFactoryMock.Object,
                _notificationServiceMock.Object,
                _userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetOrdersByClientIdAsync_ReturnsOrdersForGivenClient()
        {
            var clientId = 1;

            var mockOrders = new List<ConstructionOrder>
            {
                new ConstructionOrder
                {
                    ID = 1,
                    Description = "Test order 1",
                    Status = OrderStatus.New,
                    ConstructionType = ConstructionType.Foundation,
                    placementPhotos = new[] { "photo1.jpg", "photo2.jpg" },
                    RequestedStartTime = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
                    ClientProposedPrice = 1000m,
                    ClientId = clientId,
                    Client = new Client
                    {
                        ID = clientId,
                        ContactDetails = new ContactDetails { Phone = "+43 3432 23 332", Name = "Daniel" },
                        AddressId = 1,
                        Address = new Address { City = "TestCity" }
                    }
                }
            };

            _orderRepositoryMock.Setup(repo => repo.GetOrdersByClientIdAsync(clientId))
                .ReturnsAsync(mockOrders);

            var result = await _service.GetOrdersByClientIdAsync(clientId);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(mockOrders.First().ID, result.First().ID);
            Assert.Equal(mockOrders.First().Description, result.First().Description);
            Assert.Equal(mockOrders.First().ClientProposedPrice, result.First().ClientProposedPrice);
            Assert.Equal(mockOrders.First().Client.ContactDetails, result.First().Client.ContactDetails);

            _orderRepositoryMock.Verify(repo => repo.GetOrdersByClientIdAsync(clientId), Times.Once);
        }

        [Fact]
        public async Task GetOrdersByWorkerIdAsync_ReturnsOrdersForGivenWorker()
        {
            var workerId = 2;

            var mockOrders = new List<ConstructionOrder>
            {
                new ConstructionOrder
                {
                    ID = 1,
                    Description = "Test order 1",
                    Status = OrderStatus.NegotiationInProgress,
                    ConstructionType = ConstructionType.Painting,
                    placementPhotos = new[] { "photo1.jpg" },
                    RequestedStartTime =DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3)),
                    WorkerProposedPrice = 1500m,
                    WorkerId = workerId,
                    Worker = new Worker
                    {
                        ID = workerId,
                        ContactDetails = new ContactDetails { Phone = "+43 3432 23 332", Name = "Daniel" },
                        AddressId = 2,
                        Address = new Address { City = "WorkerCity" }
                    }, 
                    Address = new OrderAddress { ID =1 }
                }
            };

            _orderRepositoryMock.Setup(repo => repo.GetOrdersByWorkerIdAsync(workerId))
                .ReturnsAsync(mockOrders);

            var result = await _service.GetOrdersByWorkerIdAsync(workerId);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(mockOrders.First().ID, result.First().ID);
            Assert.Equal(mockOrders.First().Description, result.First().Description);
            Assert.Equal(mockOrders.First().Worker.ContactDetails, result.First().Worker.ContactDetails);

            _orderRepositoryMock.Verify(repo => repo.GetOrdersByWorkerIdAsync(workerId), Times.Once);
        }

        [Fact]
        public async Task GetAvailableOrdersAsync_ReturnsAvailableOrders()
        {
            var workerId = 1;
            var orders = new List<ConstructionOrder>
            {
                new ConstructionOrder { ID = 1, Description = "Order 1", Status = OrderStatus.New, Address = new OrderAddress { ID =1 }  },
                new ConstructionOrder { ID = 2, Description = "Order 2", Status = OrderStatus.New, Address = new OrderAddress { ID =2 }   }
            };

            _orderRepositoryMock.Setup(repo => repo.GetAvailableOrdersAsync(workerId))
                .ReturnsAsync(orders);

            var result = await _service.GetAvailableOrdersAsync(workerId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, o => o.ID == 1 && o.Description == "Order 1");
            Assert.Contains(result, o => o.ID == 2 && o.Description == "Order 2");
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnsOrder_WhenOrderExists()
        {
            var orderId = 1;
            var order = new ConstructionOrder { ID = orderId, Description = "Test Order", Address = new OrderAddress { ID = 1 } };

            _orderRepositoryMock.Setup(repo => repo.GetOrderWithSpecificationByIdAsync(orderId))
                .ReturnsAsync(order);

            var result = await _service.GetOrderByIdAsync(orderId);

            Assert.NotNull(result);
            Assert.Equal(orderId, result.ID);
            Assert.Equal("Test Order", result.Description);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ThrowsApiException_WhenOrderNotFound()
        {
            var orderId = 1;

            _orderRepositoryMock.Setup(repo => repo.GetOrderWithSpecificationByIdAsync(orderId))
                .ReturnsAsync((ConstructionOrder)null);

            var exception = await Assert.ThrowsAsync<ApiException>(() => _service.GetOrderByIdAsync(orderId));
            Assert.Equal(ErrorMessages.OrderNotFound, exception.Message);
            Assert.Equal(404, exception.StatusCode);
        }

        [Fact]
        public async Task CreateOrderAsync_CreatesOrder_WhenRequestIsValid()
        {
            var request = new CreateOrderRequest
            {
                Description = "New Order",
                ConstructionType = ConstructionType.Foundation,
                SpecificationDetails = new { Length = 10.0m, Width = 5.0m, Depth = 1.5m },
                PlacementPhotos = new[] { "photo1.jpg", "photo2.jpg" },
                RequestedStartTime = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
                ClientProposedPrice = 1000.0m,
                ClientId = 1,
                Address = new AddressDto
                {
                    City = "Warszawa",
                    PostCode = "00-001",
                    StreetName = "Marszałkowska 10"
                }
            };

            var client = new Client { ID = 1 };

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(request.ClientId))
                .ReturnsAsync(client);

            var specification = new FoundationSpecification { Length = 10.0m, Width = 5.0m, Depth = 1.5m };

            _specificationFactoryMock.Setup(factory => factory.CreateSpecification(request.ConstructionType, request.SpecificationDetails))
                .Returns(specification);

            _orderRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<ConstructionOrder>()))
                .Returns(Task.CompletedTask);

            _orderRepositoryMock.Setup(repo => repo.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            var result = await _service.CreateOrderAsync(request);

            Assert.NotNull(result);
            Assert.Equal(request.Description, result.Description);
            Assert.Equal(request.ConstructionType, result.ConstructionType);
            Assert.Equal(request.ClientId, result.Client.ID);
            Assert.NotNull(result.ConstructionSpecification);
            Assert.IsType<FoundationSpecification>(result.ConstructionSpecification);
        }

        [Fact]
        public async Task CreateOrderAsync_ThrowsApiException_WhenUserIsNotClient()
        {
            var request = new CreateOrderRequest
            {
                Description = "New Order",
                ConstructionType = ConstructionType.Foundation,
                SpecificationDetails = new { Length = 10.0m, Width = 5.0m, Depth = 1.5m },
                PlacementPhotos = new[] { "photo1.jpg", "photo2.jpg" },
                RequestedStartTime = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)),
                ClientProposedPrice = 1000.0m,
                ClientId = 2,
                Address = new AddressDto
                {
                    City = "Warszawa",
                    PostCode = "00-001",
                    StreetName = "Marszałkowska 10"
                }
            };

            var nonClientUser = new Worker { ID = 2 };

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(request.ClientId))
                .ReturnsAsync(nonClientUser);

            var exception = await Assert.ThrowsAsync<ApiException>(() => _service.CreateOrderAsync(request));
            Assert.Equal(ErrorMessages.OnlyClientCanCreateOrder, exception.Message);
            Assert.Equal(403, exception.StatusCode);
        }

        [Fact]
        public async Task AcceptOrderAsync_UpdatesOrderStatusAndSendsNotification_WhenOrderIsValid()
        {
            var orderId = 1;
            var workerId = 2;
            var order = new ConstructionOrder
            {
                ID = orderId,
                Status = OrderStatus.New,
                ClientId = 3
            };

            _orderRepositoryMock.Setup(repo => repo.GetOrderWithSpecificationByIdAsync(orderId))
                .ReturnsAsync(order);

            _notificationServiceMock.Setup(service => service.SendNotificationAsync(It.IsAny<ConstructionOrderNotification>()))
                .Returns(Task.CompletedTask);

            var result = await _service.AcceptOrderAsync(orderId, workerId);

            Assert.True(result);
            Assert.Equal(OrderStatus.Accepted, order.Status);
            Assert.Equal(workerId, order.WorkerId);

            _orderRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
            _notificationServiceMock.Verify(service => service.SendNotificationAsync(It.Is<ConstructionOrderNotification>(
                n => n.ClientId == order.ClientId &&
                     n.Title == "Order Accepted" &&
                     n.Description == "Your order has been accepted by a worker." &&
                     n.Status == NotificationStatus.OrderAccepted
            )), Times.Once);
        }

        [Fact]
        public async Task AcceptOrderAsync_ThrowsApiException_WhenOrderNotFound()
        {
            var orderId = 1;
            var workerId = 2;

            _orderRepositoryMock.Setup(repo => repo.GetOrderWithSpecificationByIdAsync(orderId))
                .ReturnsAsync((ConstructionOrder)null);

            var exception = await Assert.ThrowsAsync<ApiException>(() => _service.AcceptOrderAsync(orderId, workerId));
            Assert.Equal(ErrorMessages.InvalidOrder, exception.Message);
            Assert.Equal(StatusCodes.Status400BadRequest, exception.StatusCode);
        }

        [Fact]
        public async Task AcceptOrderAsync_ThrowsApiException_WhenOrderIsNotNew()
        {
            var orderId = 1;
            var workerId = 2;
            var order = new ConstructionOrder
            {
                ID = orderId,
                Status = OrderStatus.Accepted,
                ClientId = 3
            };

            _orderRepositoryMock.Setup(repo => repo.GetOrderWithSpecificationByIdAsync(orderId))
                .ReturnsAsync(order);

            var exception = await Assert.ThrowsAsync<ApiException>(() => _service.AcceptOrderAsync(orderId, workerId));
            Assert.Equal(ErrorMessages.InvalidOrder, exception.Message);
            Assert.Equal(StatusCodes.Status400BadRequest, exception.StatusCode);
        }

        [Fact]
        public async Task DeleteOrderAsync_DeletesOrder_WhenClientIsAuthorized()
        {
            var clientId = 3;
            var orderId = 1;
            var order = new ConstructionOrder
            {
                ID = orderId,
                ClientId = clientId
            };

            _orderRepositoryMock.Setup(repo => repo.GetOrderWithSpecificationByIdAsync(orderId))
                .ReturnsAsync(order);

            _orderRepositoryMock.Setup(repo => repo.DeleteAsync(order))
                .Returns(Task.CompletedTask);

            var result = await _service.DeleteOrderAsync(clientId, orderId);

            Assert.True(result);

            _orderRepositoryMock.Verify(repo => repo.DeleteAsync(order), Times.Once);
            _orderRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteOrderAsync_ThrowsApiException_WhenOrderNotFound()
        {
            var clientId = 3;
            var orderId = 1;

            _orderRepositoryMock.Setup(repo => repo.GetOrderWithSpecificationByIdAsync(orderId))
                .ReturnsAsync((ConstructionOrder)null);

            var exception = await Assert.ThrowsAsync<ApiException>(() => _service.DeleteOrderAsync(clientId, orderId));
            Assert.Equal(ErrorMessages.OrderNotFound, exception.Message);
            Assert.Equal(StatusCodes.Status404NotFound, exception.StatusCode);
        }

        [Fact]
        public async Task DeleteOrderAsync_ThrowsApiException_WhenClientIsNotAuthorized()
        {
            var clientId = 3;
            var orderId = 1;
            var order = new ConstructionOrder
            {
                ID = orderId,
                ClientId = 99
            };

            _orderRepositoryMock.Setup(repo => repo.GetOrderWithSpecificationByIdAsync(orderId))
                .ReturnsAsync(order);

            var exception = await Assert.ThrowsAsync<ApiException>(() => _service.DeleteOrderAsync(clientId, orderId));
            Assert.Equal(ErrorMessages.UnauthorizedOrderAccess, exception.Message);
            Assert.Equal(StatusCodes.Status403Forbidden, exception.StatusCode);
        }

    }
}
