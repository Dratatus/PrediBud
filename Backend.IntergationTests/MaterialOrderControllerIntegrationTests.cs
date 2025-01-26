using Backend.DTO.MaterialOrder;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using Backend.Data.Consts;

namespace Backend.IntergationTests
{
    public class MaterialOrderControllerIntegrationTests : IClassFixture<TestDbContextFactory>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public MaterialOrderControllerIntegrationTests(TestDbContextFactory factory)
        {
            _client = factory.CreateClient();
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
        }

        [Fact]
        public async Task GetAllOrders_ReturnsOrders()
        {
            var response = await _client.GetAsync("/api/MaterialOrder");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var orders = JsonSerializer.Deserialize<List<MaterialOrderDto>>(responseBody, _jsonOptions);

            Assert.NotNull(orders);
            Assert.True(orders.Count > 0);
        }

        [Fact]
        public async Task GetOrderById_ReturnsOrder_WhenValidId()
        {
            var orderId = 104; 

            var response = await _client.GetAsync($"/api/MaterialOrder/{orderId}");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var order = JsonSerializer.Deserialize<MaterialOrderDto>(responseBody, _jsonOptions);

            Assert.NotNull(order);
            Assert.Equal(orderId, order.ID);
        }

        [Fact]
        public async Task GetOrderById_ReturnsNotFound_WhenInvalidId()
        {
            var orderId = 999; 

            var response = await _client.GetAsync($"/api/MaterialOrder/{orderId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();

            Assert.Contains($"Order with ID {orderId} not found.", responseBody);
        }

        [Fact]
        public async Task CreateOrder_ReturnsCreatedOrder()
        {
            var request = new
            {
                UnitPriceNet = 10.0m,
                UnitPriceGross = 12.3m,
                Quantity = 5,
                UserId = 21, 
                Supplier = new { ID = 2, Name = "Supplier 1" },
                SupplierId = 2,
                MaterialPriceId = 1
            };

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync("/api/MaterialOrder", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();

            var createdOrder = JsonSerializer.Deserialize<MaterialOrderDto>(responseBody, _jsonOptions);

            Assert.NotNull(createdOrder);
            Assert.Equal(request.UnitPriceNet, createdOrder.UnitPriceNet);
            Assert.Equal(request.Quantity, createdOrder.Quantity);
        }

        [Fact]
        public async Task UpdateOrder_ReturnsNoContent_WhenSuccessful()
        {
            var request = new
            {
                ID = 101, 
                UnitPriceNet = 15.0m,
                UnitPriceGross = 18.45m,
                Quantity = 10,
                MaterialPriceId = 2,
                SupplierId = 2,
            };

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var userId = 21;

            var response = await _client.PutAsync($"/api/MaterialOrder?userId={userId}", content);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task UpdateOrder_ReturnsForbidden_WhenNotOwner()
        {
            var request = new
            {
                ID = 101, 
                UnitPriceNet = 15.0m,
                UnitPriceGross = 18.45m,
                Quantity = 10,
                MaterialPriceId = 2,
                SupplierId = 2,
            };

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var userId = 999; 

            var response = await _client.PutAsync($"/api/MaterialOrder?userId={userId}", content);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();

            var errorResponse = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);

            Assert.NotNull(errorResponse);
            Assert.Contains(ErrorMessages.UnauthorizedAccess, errorResponse["error"].GetString());
        }

        [Fact]
        public async Task DeleteOrder_ReturnsNoContent_WhenSuccessful()
        {
            var orderId = 101;
            var userId = 21; 

            var response = await _client.DeleteAsync($"/api/MaterialOrder/{orderId}?userId={userId}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteOrder_ReturnsForbidden_WhenNotOwner()
        {
            var orderId = 101; 
            var userId = 999; 

            var response = await _client.DeleteAsync($"/api/MaterialOrder/{orderId}?userId={userId}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();

            var errorResponse = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);

            Assert.NotNull(errorResponse);
            Assert.Contains(ErrorMessages.UnauthorizedAccess, errorResponse["error"].GetString());
        }
    }
}
