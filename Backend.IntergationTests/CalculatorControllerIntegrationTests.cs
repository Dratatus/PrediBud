using Backend.Data.Models.Constructions.Dimensions.Balcony;
using Backend.Data.Models.Price;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Backend.Tests.Integration.Controllers
{
    public class CalculatorControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CalculatorControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CalculatePrice_ReturnsCalculatedPrice_ForValidSpecification()
        {
            // Arrange
            var balconySpecification = new
            {
                Type = "Balcony",
                Length = 5.0m,
                Width = 3.0m,
                RailingMaterial = "Steel"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(balconySpecification),
                Encoding.UTF8,
                "application/json"
            );

            // Act
            var response = await _client.PostAsync("/api/Calculator/calculate", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var calculatedPrice = JsonSerializer.Deserialize<CalculatedPrice>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(calculatedPrice);
            Assert.True(calculatedPrice.PriceWithoutTax > 0);
            Assert.True(calculatedPrice.PriceWithTax > 0);
        }

        [Fact]
        public async Task CalculatePrice_ReturnsBadRequest_ForUnsupportedSpecification()
        {
            // Arrange
            var unsupportedSpecification = new
            {
                Type = "UnsupportedType"
            };

            var content = new StringContent(
                JsonSerializer.Serialize(unsupportedSpecification),
                Encoding.UTF8,
                "application/json"
            );

            // Act
            var response = await _client.PostAsync("/api/Calculator/calculate", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();

            var errorResponse = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);

            Assert.NotNull(errorResponse);
            Assert.True(errorResponse.ContainsKey("error"));
            Assert.Contains("Unsupported ConstructionType", errorResponse["error"].GetString());
            Assert.True(errorResponse.ContainsKey("statusCode"));
            Assert.Equal(400, errorResponse["statusCode"].GetInt32());
        }

        [Fact]
        public async Task CalculatePrice_ReturnsBadRequest_ForInvalidSpecification()
        {
            // Arrange
            var invalidSpecification = new
            {
                Type = "InvalidType",
                Height = 3.0m,
                Width = 4.0m
            };

            var content = new StringContent(
                JsonSerializer.Serialize(invalidSpecification),
                Encoding.UTF8,
                "application/json"
            );

            // Act
            var response = await _client.PostAsync("/api/Calculator/calculate", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();

            var errorResponse = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);

            Assert.NotNull(errorResponse);
            Assert.True(errorResponse.ContainsKey("error"));
            Assert.Contains("Unsupported ConstructionType", errorResponse["error"].GetString());
            Assert.True(errorResponse.ContainsKey("statusCode"));
            Assert.Equal(400, errorResponse["statusCode"].GetInt32());
        }
    }
}
