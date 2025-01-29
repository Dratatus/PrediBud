using Backend.DTO.ConstructionOrderDto;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using Backend.Conventer;
using Backend.IntergationTests.Data;

namespace Backend.IntergationTests.Controllers
{
    public class ConstructionOrderClientControllerIntegrationTests : IClassFixture<TestDbContextFactory>
    {
        private readonly HttpClient _client;

        public ConstructionOrderClientControllerIntegrationTests(TestDbContextFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllOrders_ReturnsOrders_ForValidClientId()
        {
            var clientId = 21;

            var response = await _client.GetAsync($"/api/ConstructionOrderClient/all/{clientId}");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new ConstructionSpecificationJsonConverter(), new JsonStringEnumConverter() }
            };

            var orders = JsonSerializer.Deserialize<List<ConstructionOrderDto>>(responseBody, options);

            Assert.NotNull(orders);
            Assert.True(orders.Count > 0);
        }

        [Fact]
        public async Task GetOrder_ReturnsOrder_ForValidOrderId()
        {
            var orderId = 10;

            var response = await _client.GetAsync($"/api/ConstructionOrderClient/{orderId}");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new ConstructionSpecificationJsonConverter(), new JsonStringEnumConverter() }
            };

            var order = JsonSerializer.Deserialize<ConstructionOrderDto>(responseBody, options);

            Assert.NotNull(order);
            Assert.Equal(orderId, order.ID);
        }

        [Fact]
        public async Task CreateOrder_ReturnsCreatedOrder_ForValidRequest()
        {

            var request = new
            {
                Description = "Instalacja okien na parterze",
                ConstructionType = 2,
                SpecificationDetails = new
                {
                    Amount = 5.0,
                    Height = 3.0,
                    Width = 1.2,
                    Material = 3,
                },
                PlacementPhotos = new[] { "photo1.jpg", "photo2.jpg" },
                RequestedStartTime = DateTime.UtcNow,
                ClientProposedPrice = 1500.0m,
                ClientId = 1
            };

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync("/api/ConstructionOrderClient", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter(), new ConstructionSpecificationJsonConverter() }
            };

            var responseBody = await response.Content.ReadAsStringAsync();
            var createdOrder = JsonSerializer.Deserialize<ConstructionOrderDto>(responseBody, options);

            Assert.NotNull(createdOrder);
            Assert.Equal(request.Description, createdOrder.Description);
            Assert.Equal(request.ClientId, createdOrder.ClientId);
        }

        [Fact]
        public async Task DeleteOrder_ReturnsNoContent_ForValidClientAndOrderId()
        {
            var clientId = 1;
            var orderId = 10;

            var response = await _client.DeleteAsync($"/api/ConstructionOrderClient/{orderId}/{clientId}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOrder_ReturnsForbidden_ForInvalidClient()
        {
            var clientId = 999;
            var orderId = 77;

            var response = await _client.DeleteAsync($"/api/ConstructionOrderClient/{orderId}/{clientId}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();

            var errorResponse = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);

            Assert.NotNull(errorResponse);
            Assert.True(errorResponse.ContainsKey("error"));
            Assert.Contains("You do not have permission to access this order", errorResponse["error"].GetString());
            Assert.True(errorResponse.ContainsKey("statusCode"));
            Assert.Equal(403, errorResponse["statusCode"].GetInt32());
        }
    }
}