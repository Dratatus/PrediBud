using Backend.Data.Models.Common;
using Backend.Data.Models.Orders.Material; 
using Backend.Data.Models.Suppliers;
using Backend.DTO.MaterialOrder; 
using Backend.DTO.Users.Supplier;
using Backend.Repositories; 
using Backend.services; 
using Moq;

namespace Backend.Tests.Services
{
    public class MaterialOrderServiceTests
    {
        private readonly Mock<IMaterialOrderRepository> _mockRepo;
        private readonly Mock<IUserRepository> _mockRepo2;
        private readonly MaterialOrderService _service;

        public MaterialOrderServiceTests()
        {
            _mockRepo = new Mock<IMaterialOrderRepository>();
            _mockRepo2 = new Mock<IUserRepository>();

            _service = new MaterialOrderService(_mockRepo.Object, _mockRepo2.Object);
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
                Supplier = new SupplierDto
                {
                    Name = "BestMaterials",
                    ContactEmail = "info@test.com",
                },
                MaterialPriceId = 1380
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

            _mockRepo.Setup(r => r.AddMaterialOrderAsync(It.IsAny<MaterialOrder>()))
                     .Callback<MaterialOrder>(order => order.ID = 123) 
                     .Returns(Task.CompletedTask);

            _mockRepo.Setup(r => r.GetMaterialOrderByIdAsync(123))
                     .ReturnsAsync(createdEntity);

            var resultDto = await _service.CreateMaterialOrderAsync(inputDto);

            _mockRepo.Verify(r => r.AddMaterialOrderAsync(It.IsAny<MaterialOrder>()),
                             Times.Once);

            _mockRepo.Verify(r => r.GetMaterialOrderByIdAsync(123), 
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
            _mockRepo.Setup(r => r.GetMaterialOrderByIdAsync(orderId))
                     .ReturnsAsync((MaterialOrder)null);

            var result = await _service.GetMaterialOrderByIdAsync(orderId);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateMaterialOrderAsync_ReturnsFalse_IfEntityNotFound()
        {
            //var dto = new MaterialOrderDto { ID = 888 };
            //_mockRepo.Setup(r => r.GetMaterialOrderByIdAsync(dto.ID))
            //         .ReturnsAsync((MaterialOrder)null);

            //var success = await _service.UpdateMaterialOrderAsync(dto);

            //Assert.False(success);
            //_mockRepo.Verify(r => r.UpdateMaterialOrderAsync(It.IsAny<MaterialOrder>()),
            //                 Times.Never);
        }

        [Fact]
        public async Task UpdateMaterialOrderAsync_UpdatesEntity_IfFound()
        {
            // Arrange
            var dto = new MaterialOrderDto
            {
                ID = 777,
                UnitPriceNet = 100,
                UnitPriceGross = 123,
                Quantity = 2,
                CreatedDate = new System.DateTime(2025, 1, 20),
                UserId = 999,
                MaterialPriceId = 55
            };
            var existing = new MaterialOrder
            {
                ID = 777
            };

            _mockRepo.Setup(r => r.GetMaterialOrderByIdAsync(dto.ID))
                     .ReturnsAsync(existing);

            //// Act
            //var success = await _service.UpdateMaterialOrderAsync(dto);

            //// Assert
            //Assert.True(success);
            //_mockRepo.Verify(r => r.UpdateMaterialOrderAsync(It.Is<MaterialOrder>(
            //    mo => mo.ID == 777
            //       && mo.UnitPriceNet == 100
            //       && mo.UnitPriceGross == 123
            //       && mo.Quantity == 2
            //       && mo.CreatedDate == new System.DateTime(2025, 1, 20)
            //       && mo.UserId == 999
            //       && mo.MaterialPriceId == 55
            //)), Times.Once);
        }

        [Fact]
        public async Task DeleteMaterialOrderAsync_ReturnsFalse_IfNotFound()
        {
            // Arrange
           // int orderId = 666;
           // _mockRepo.Setup(r => r.GetMaterialOrderByIdAsync(orderId))
           //          .ReturnsAsync((MaterialOrder)null);

           // Act
           //var success = await _service.DeleteMaterialOrderAsync(orderId);

           // Assert
           // Assert.False(success);
           // _mockRepo.Verify(r => r.DeleteMaterialOrderAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DeleteMaterialOrderAsync_Deletes_IfFound()
        {
            // Arrange
            //int orderId = 11;
            //var existing = new MaterialOrder
            //{
            //    ID = orderId
            //};
            //_mockRepo.Setup(r => r.GetMaterialOrderByIdAsync(orderId))
            //         .ReturnsAsync(existing);

            //// Act
            //var success = await _service.DeleteMaterialOrderAsync(orderId);

            //// Assert
            //Assert.True(success);
            //_mockRepo.Verify(r => r.DeleteMaterialOrderAsync(orderId), Times.Once);
        }
    }
}
