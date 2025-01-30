using Backend.Data.Consts;
using Backend.Data.Models.Common;
using Backend.Data.Models.Orders.Material;
using Backend.Data.Models.Suppliers;
using Backend.DTO.Orders.Material;
using Backend.DTO.Users.Supplier;
using Backend.Middlewares;
using Backend.Repositories;
using Backend.services.Material;
using Moq;

namespace Backend.Tests.Services
{
    public class MaterialOrderServiceTests
    {
        private readonly Mock<IMaterialOrderRepository> _repositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMaterialPriceRepository> _materialPriceRepositoryMock;
        private readonly MaterialOrderService _service;

        public MaterialOrderServiceTests()
        {
            _repositoryMock = new Mock<IMaterialOrderRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _materialPriceRepositoryMock = new Mock<IMaterialPriceRepository>();

            _service = new MaterialOrderService(_repositoryMock.Object, _userRepositoryMock.Object, _materialPriceRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateMaterialOrderAsync_ShouldCreateAndReturnDto()
        {
            var inputDto = new MaterialOrderDto
            {
                ID = 0,
                UnitPriceNet = 528.46m,
                UnitPriceGross = 650m,
                Quantity = 10,
                CreatedDate = new System.DateTime(2025, 1, 16, 21, 39, 16),
                UserId = 1,
                SupplierId = 45,
                //Supplier = new SupplierDto
                //{
                //    Name = "BestMaterials",
                //    ContactEmail = "info@test.com",
                //},
                //MaterialPriceId = 1380
            };

            var createdEntity = new MaterialOrder
            {
                ID = 123,
                UnitPriceNet = 528.46m,
                UnitPriceGross = 650m,
                Quantity = 10,
                CreatedDate = new System.DateTime(2025, 1, 16, 21, 39, 16),
                UserId = 1,
                SupplierId = 45,
                Supplier = new Supplier
                {
                    ID = 45,
                    Name = "BestMaterials",
                    ContactEmail = "info@test.com",
                    Address = new Address()
                },
                MaterialPriceId = 1380
            };

            _repositoryMock.Setup(r => r.AddMaterialOrderAsync(It.IsAny<MaterialOrder>()))
                     .Callback<MaterialOrder>(order => order.ID = 123)
                     .Returns(Task.CompletedTask);

            _repositoryMock.Setup(r => r.GetMaterialOrderByIdAsync(123))
                     .ReturnsAsync(createdEntity);

            var resultDto = await _service.CreateMaterialOrderAsync(inputDto);

            _repositoryMock.Verify(r => r.AddMaterialOrderAsync(It.IsAny<MaterialOrder>()),
                             Times.Once);

            _repositoryMock.Verify(r => r.GetMaterialOrderByIdAsync(123),
                             Times.Once);

            Assert.NotNull(resultDto);
            Assert.Equal(123, resultDto.ID);
            Assert.Equal(650m, resultDto.UnitPriceGross);
            Assert.Equal(10, resultDto.Quantity);
            Assert.Equal(1, resultDto.UserId);
            Assert.Equal(45, resultDto.SupplierId);
            Assert.Equal(1380, resultDto.MaterialPriceId);

            Assert.Equal(528.46m * 10, resultDto.TotalPriceNet);
            Assert.Equal(650m * 10, resultDto.TotalPriceGross);
        }

        [Fact]
        public async Task GetMaterialOrderByIdAsync_ReturnsNull_IfNotFound()
        {
            int orderId = 999;
            _repositoryMock.Setup(r => r.GetMaterialOrderByIdAsync(orderId))
                     .ReturnsAsync((MaterialOrder)null);

            var result = await _service.GetMaterialOrderByIdAsync(orderId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetMaterialOrderByIdAsync_ShouldReturnDto()
        {
            var orderId = 1;
            var materialOrder = new MaterialOrder
            {
                ID = orderId,
                UnitPriceNet = 100m,
                UnitPriceGross = 123m,
                Quantity = 10,
                UserId = 1,
                SupplierId = 2,
                Supplier = new Supplier
                {
                    Name = "Test Supplier",
                    ContactEmail = "supplier@example.com",
                    Address = new Address
                    {
                        City = "Test City",
                        StreetName = "Test Street",
                        PostCode = "12345"
                    }
                },
                MaterialPriceId = 5,
                MaterialPrice = new MaterialPrice
                {
                    PriceWithoutTax = 12.5m,
                    SupplierId = 2
                }
            };

            _repositoryMock.Setup(r => r.GetMaterialOrderByIdAsync(orderId))
                .ReturnsAsync(materialOrder);

            var result = await _service.GetMaterialOrderByIdAsync(orderId);

            Assert.NotNull(result);
            Assert.Equal(orderId, result.ID);
            Assert.Equal(materialOrder.UnitPriceNet, result.UnitPriceNet);
            _repositoryMock.Verify(r => r.GetMaterialOrderByIdAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task UpdateMaterialOrderAsync_ShouldUpdateOrder_WhenValid()
        {
            var userId = 1;
            var updateDto = new UpdateMaterialOrderDto
            {
                ID = 1,
                UnitPriceNet = 150m,
                UnitPriceGross = 180m,
                Quantity = 5,
                SupplierId = 2,
                MaterialPriceId = 10
            };

            var existingOrder = new MaterialOrder
            {
                ID = updateDto.ID,
                UnitPriceNet = 100m,
                UnitPriceGross = 123m,
                Quantity = 10,
                UserId = userId,
                SupplierId = 2,
                MaterialPriceId = 5
            };

            _repositoryMock.Setup(r => r.GetMaterialOrderByIdAsync(updateDto.ID))
                .ReturnsAsync(existingOrder);

            _repositoryMock.Setup(r => r.UpdateMaterialOrderAsync(existingOrder))
                .Returns(Task.CompletedTask);

            var result = await _service.UpdateMaterialOrderAsync(updateDto, userId);

            Assert.True(result);
            Assert.Equal(updateDto.UnitPriceNet, existingOrder.UnitPriceNet);
            Assert.Equal(updateDto.Quantity, existingOrder.Quantity);
            _repositoryMock.Verify(r => r.GetMaterialOrderByIdAsync(updateDto.ID), Times.Once);
            _repositoryMock.Verify(r => r.UpdateMaterialOrderAsync(existingOrder), Times.Once);
        }

        [Fact]
        public async Task DeleteMaterialOrderAsync_ShouldDeleteOrder_WhenValid()
        {
            var orderId = 1;
            var userId = 1;

            var existingOrder = new MaterialOrder
            {
                ID = orderId,
                UserId = userId
            };

            _repositoryMock.Setup(r => r.GetMaterialOrderByIdAsync(orderId))
                .ReturnsAsync(existingOrder);

            _repositoryMock.Setup(r => r.DeleteMaterialOrderAsync(orderId))
                .Returns(Task.CompletedTask);

            var result = await _service.DeleteMaterialOrderAsync(orderId, userId);

            Assert.True(result);
            _repositoryMock.Verify(r => r.GetMaterialOrderByIdAsync(orderId), Times.Once);
            _repositoryMock.Verify(r => r.DeleteMaterialOrderAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task DeleteMaterialOrderAsync_ShouldThrowException_WhenOrderNotFound()
        {
            var orderId = 1;
            var userId = 1;

            _repositoryMock.Setup(r => r.GetMaterialOrderByIdAsync(orderId))
                .ReturnsAsync((MaterialOrder)null);

            var exception = await Assert.ThrowsAsync<ApiException>(() => _service.DeleteMaterialOrderAsync(orderId, userId));
            Assert.Equal(ErrorMessages.MaterialOrderNotFound, exception.Message);
            Assert.Equal(404, exception.StatusCode);
        }
    }
}
