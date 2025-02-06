using Backend.Conventer;
using Backend.Data.Consts;
using Backend.IntergationTests.Data;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Backend.IntergationTests.Controllers
{
    public class NegotiationControllerIntegrationTests : IClassFixture<TestDbContextFactory>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public NegotiationControllerIntegrationTests(TestDbContextFactory factory)
        {
            _client = factory.CreateClient();

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new JsonStringEnumConverter(),
                    new ConstructionSpecificationJsonConverter()
                }
            };
        }

        private async Task AssertApiErrorResponse(HttpResponseMessage response,
                                                  HttpStatusCode expectedStatusCode,
                                                  string expectedErrorMessagePart)
        {
            Assert.Equal(expectedStatusCode, response.StatusCode);

            var respString = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(respString, _jsonOptions);

            Assert.NotNull(errorResponse);
            Assert.True(errorResponse.ContainsKey("error"));
            Assert.Contains(expectedErrorMessagePart, errorResponse["error"].GetString());
            Assert.True(errorResponse.ContainsKey("statusCode"));
            Assert.Equal((int)expectedStatusCode, errorResponse["statusCode"].GetInt32());
        }


        [Fact]
        public async Task InitiateNegotiation_ReturnsOk_WhenOrderIsNew()
        {
            var orderId = 71;
            var requestBody = new { WorkerId = 20, ProposedPrice = 1500m };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/Negotiation/{orderId}/initiate", content);

            response.EnsureSuccessStatusCode();
            var respString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Negotiation has been initiated", respString);
        }

        [Fact]
        public async Task InitiateNegotiation_ReturnsBadRequest_WhenOrderNotNew()
        {
            var orderId = 74;
            var requestBody = new { WorkerId = 20, ProposedPrice = 2500m };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/Negotiation/{orderId}/initiate", content);

            await AssertApiErrorResponse(response, HttpStatusCode.BadRequest, ErrorMessages.OrderNotNew);
        }

        [Fact]
        public async Task AcceptNegotiation_ReturnsOk_WhenOrderIsNegotiationInProgress()
        {
            var orderId = 72;
            var requestBody = new { ClientId = 21 };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/Negotiation/{orderId}/accept", content);

            response.EnsureSuccessStatusCode();
            var respString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Negotiation has been accepted", respString);
        }

        [Fact]
        public async Task RejectNegotiation_ReturnsOk_WhenOrderIsNegotiationInProgress()
        {
            var orderId = 77;
            var requestBody = new { UserId = 20 };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/Negotiation/{orderId}/reject", content);

            response.EnsureSuccessStatusCode();
            var respString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Negotiation has been rejected", respString);
        }

        [Fact]
        public async Task ContinueNegotiation_ReturnsOk_WhenOrderIsNegotiationInProgress()
        {
            var orderId = 76;
            var requestBody = new { UserId = 21, ProposedPrice = 3000m };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/Negotiation/{orderId}/continue", content);

            response.EnsureSuccessStatusCode();
            var respString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Negotiation has been continued", respString);
        }

        [Fact]
        public async Task CompleteOrder_ReturnsOk_WhenOrderIsAccepted()
        {
            var orderId = 73;
            var requestBody = new { UserId = 21 };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/Negotiation/{orderId}/complete", content);

            response.EnsureSuccessStatusCode();
            var respString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Negotiation has been completed", respString);
        }

        [Fact]
        public async Task CompleteOrder_ReturnsBadRequest_WhenOrderNotAccepted()
        {
            var orderId = 75;
            var requestBody = new { UserId = 21 };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/Negotiation/{orderId}/complete", content);

            await AssertApiErrorResponse(response, HttpStatusCode.BadRequest, ErrorMessages.OrderNotAccepted);
        }
    }
}
