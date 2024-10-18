using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace Backend.IntergationTests
{
    public class AuthControllerIntegrationTests : IClassFixture<TestDbContextFactory>
    {
        private readonly HttpClient _client;

        public AuthControllerIntegrationTests(TestDbContextFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Register_ReturnsSuccess_WhenUserIsCreated()
        {
            var requestBody = new
            {
                email = "newuser@example.com",
                password = "password123",
                isClient = true,
                name = "New User",
                phone = "123456789",
                position = (string)null
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(requestBody),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync("/api/auth/register", content);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("token", responseString);
        }
    }
}