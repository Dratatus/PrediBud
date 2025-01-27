using Backend.Data.Models.Common;
using Backend.DTO.Auth;
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
            var requestBody = new RegisterUserBody
            {
                Email = "newuser1@example.com",
                Password = "password",
                IsClient = true,
                Name = "Daniel Orban",
                Phone = "+23 4212231 23",
                Position = "Client",
                Address = new Address
                {
                    City = "Kraków",
                    PostCode = "33-200",
                    StreetName = "Wielicka 12B"
                }
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