using Backend.Data.Consts;
using Backend.Data.Models.Common;
using Backend.Data.Models.Credidentials;
using Backend.Data.Models.Users;
using Backend.DTO.Auth;
using Backend.Middlewares;
using Backend.Repositories;
using Backend.services.Auth;
using Backend.services.Token;
using Backend.Validatiors.Login;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Backend.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
        private readonly Mock<IPasswordValidation> _passwordValidationMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _passwordValidationMock = new Mock<IPasswordValidation>();

            _authService = new AuthService(
                _userRepositoryMock.Object,
                _passwordValidationMock.Object,
                _jwtTokenGeneratorMock.Object
            );
        }

        [Fact]
        public async Task LoginAsync_ReturnsSuccess_WhenCredentialsAreValid()
        {
            var loginBody = new LoginBody
            {
                Email = "test@example.com",
                Password = "password"
            };

            var user = new User
            {
                Credentials = new Credentials { Email = loginBody.Email, PasswordHash = "hashedpassword" }
            };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(loginBody.Email))
                .ReturnsAsync(user);

            _passwordValidationMock.Setup(pv => pv.ValidatePassword(loginBody.Password, user.Credentials.PasswordHash))
                .Returns(true);

            _jwtTokenGeneratorMock.Setup(jwt => jwt.GenerateToken(user))
                .Returns("token");

            var result = await _authService.LoginAsync(loginBody);

            Assert.True(result.Success);
            Assert.Equal("token", result.Token);
        }

        [Fact]
        public async Task LoginAsync_ThrowsApiException_WhenUserNotFound()
        {
            var loginBody = new LoginBody { Email = "test@example.com", Password = "password" };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(loginBody.Email))
                .ReturnsAsync((User)null);

            var exception = await Assert.ThrowsAsync<ApiException>(() => _authService.LoginAsync(loginBody));
            Assert.Equal(ErrorMessages.InvalidEmailOrPassword, exception.Message);
            Assert.Equal(StatusCodes.Status401Unauthorized, exception.StatusCode);
        }

        [Fact]
        public async Task LoginAsync_ThrowsApiException_WhenPasswordIsInvalid()
        {
            var loginBody = new LoginBody { Email = "test@example.com", Password = "password" };

            var user = new User
            {
                Credentials = new Credentials { Email = loginBody.Email, PasswordHash = "hashedpassword" }
            };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(loginBody.Email))
                .ReturnsAsync(user);

            _passwordValidationMock.Setup(pv => pv.ValidatePassword(loginBody.Password, user.Credentials.PasswordHash))
                .Returns(false);

            var exception = await Assert.ThrowsAsync<ApiException>(() => _authService.LoginAsync(loginBody));
            Assert.Equal(ErrorMessages.InvalidEmailOrPassword, exception.Message);
            Assert.Equal(StatusCodes.Status401Unauthorized, exception.StatusCode);
        }

        [Fact]
        public async Task RegisterAsync_ReturnsSuccess_WhenUserIsRegisteredSuccessfully()
        {
            var registerBody = new RegisterUserBody
            {
                Email = "newuser@example.com",
                Password = "password",
                IsClient = true, 
                Name = "Daniel Orban",
                Phone = "+23 4212231 23",
                Address = new Address { 
                    City = "Kraków", 
                    PostCode = "33-200",
                    StreetName = "Wielicka 12B"
                }
            };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(registerBody.Email))
                .ReturnsAsync((User)null);

            _jwtTokenGeneratorMock.Setup(jwt => jwt.GenerateToken(It.IsAny<User>()))
                .Returns("token");

            _userRepositoryMock.Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var result = await _authService.RegisterAsync(registerBody);

            Assert.True(result.Success);
            Assert.Equal("token", result.Token);
        }

        [Fact]
        public async Task RegisterAsync_ThrowsApiException_WhenUserAlreadyExists()
        {
            var registerBody = new RegisterUserBody
            {
                Email = "newuser@example.com",
                Password = "password",
                IsClient = true,
                Name = "Daniel Orban",
                Phone = "+23 4212231 23",
                Address = new Address
                {
                    City = "Kraków",
                    PostCode = "33-200",
                    StreetName = "Wielicka 12B"
                }
            };

            var existingUser = new User
            {
                Credentials = new Credentials { Email = registerBody.Email }
            };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(registerBody.Email))
                .ReturnsAsync(existingUser);

            var exception = await Assert.ThrowsAsync<ApiException>(() => _authService.RegisterAsync(registerBody));
            Assert.Equal(ErrorMessages.UserAlreadyExists, exception.Message);
            Assert.Equal(StatusCodes.Status400BadRequest, exception.StatusCode);
        }

        [Fact]
        public async Task DeleteUserAsync_Success_WhenUserIsDeleted()
        {
            var userId = 1;
            var user = new User { ID = userId };

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.HasMaterialOrdersAsync(userId))
                .ReturnsAsync(false);
            _userRepositoryMock.Setup(repo => repo.HasConstructionOrdersAsync(userId))
                .ReturnsAsync(false);
            _userRepositoryMock.Setup(repo => repo.DeleteUserAsync(userId))
                .ReturnsAsync(true);

            await _authService.DeleteUserAsync(userId);

            _userRepositoryMock.Verify(repo => repo.DeleteUserAsync(userId), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_ThrowsApiException_WhenUserNotFound()
        {
            var userId = 1;

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync((User)null);

            var exception = await Assert.ThrowsAsync<ApiException>(() => _authService.DeleteUserAsync(userId));
            Assert.Equal(ErrorMessages.UserNotFound, exception.Message);
            Assert.Equal(StatusCodes.Status404NotFound, exception.StatusCode);
        }
    }
}