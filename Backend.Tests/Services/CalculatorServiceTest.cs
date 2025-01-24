using Backend.Data.Consts;
using Backend.Data.Context;
using Backend.Data.Models.Constructions;
using Backend.Data.Models.Constructions.Dimensions.Balcony;
using Backend.Data.Models.Constructions.Specyfication;
using Backend.Data.Models.Constructions.Specyfication.Ceiling;
using Backend.Data.Models.MaterialPrices.Balcony;
using Backend.Data.Models.MaterialPrices.ShellOpen;
using Backend.Data.Models.Suppliers;
using Backend.Middlewares;
using Backend.services.Calculator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Backend.Tests.Services
{
    public class CalculatorServiceTest
    {
        private readonly PrediBudDBContext _dbContext;
        private readonly CalculatorService _calculatorService;

        public CalculatorServiceTest()
        {
            var options = new DbContextOptionsBuilder<PrediBudDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new PrediBudDBContext(options);
            _calculatorService = new CalculatorService(_dbContext);
        }

        [Fact]
        public async Task CalculatePriceAsync_ReturnsCorrectPrice_ForBalconySpecification()
        {
            var specification = new BalconySpecification
            {
                Length = 10,
                Width = 5,
                RailingMaterial = RailingMaterial.Steel
            };

            var materialPrice = new BalconyMaterialPrice
            {
                MaterialType = MaterialType.Steel,
                MaterialCategory = ConstructionType.Balcony,
                PricePerMeter = 50
            };

            await _dbContext.MaterialPrices.AddAsync(materialPrice);
            await _dbContext.SaveChangesAsync();

            var result = await _calculatorService.CalculatePriceAsync(specification);

            Assert.Equal(1500m, result.PriceWithoutTax); 
            Assert.Equal(1845m, result.PriceWithTax); 
        }

        [Fact]
        public async Task CalculatePriceAsync_ReturnsCorrectPrice_ForCeilingSpecification()
        {
            var specification = new CeilingSpecification
            {
                Area = 100,
                Material = CeilingMaterial.Concrete
            };

            var materialPrice = new CeilingMaterialPrice
            {
                MaterialType = MaterialType.Concrete,
                MaterialCategory = ConstructionType.Ceiling,
                PricePerSquareMeter = 40
            };

            await _dbContext.MaterialPrices.AddAsync(materialPrice);
            await _dbContext.SaveChangesAsync();

            var result = await _calculatorService.CalculatePriceAsync(specification);

            Assert.Equal(4000m, result.PriceWithoutTax); 
            Assert.Equal(4920m, result.PriceWithTax); 
        }

        [Fact]
        public async Task CalculatePriceAsync_ThrowsApiException_WhenSpecificationTypeNotSupported()
        {
            var specification = new UnsupportedSpecification();

            var exception = await Assert.ThrowsAsync<ApiException>(() => _calculatorService.CalculatePriceAsync(specification));

            var expectedMessage = $"Specification type {typeof(UnsupportedSpecification)} is not supported.";
            Assert.Equal(expectedMessage, exception.Message);
            Assert.Equal(StatusCodes.Status401Unauthorized, exception.StatusCode);
        }

        [Fact]
        public async Task CalculatePriceAsync_ThrowsApiException_WhenMaterialPriceNotFound()
        {
            var specification = new BalconySpecification
            {
                Length = 10,
                Width = 5,
                RailingMaterial = RailingMaterial.Wood
            };


            var exception = await Assert.ThrowsAsync<ApiException>(() => _calculatorService.CalculatePriceAsync(specification));
            Assert.Equal(ErrorMessages.MaterialPriceNotFound, exception.Message);
            Assert.Equal(StatusCodes.Status404NotFound, exception.StatusCode);
        }

        public class UnsupportedSpecification : ConstructionSpecification
        {
            public UnsupportedSpecification()
            {
                Type = (ConstructionType)(-1); 
            }
        }
    }
}