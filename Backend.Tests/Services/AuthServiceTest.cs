using Backend.Data.Models.Credidentials;
using Backend.Data.Models.Users;
using Backend.DTO.Auth;
using Backend.Repositories;
using Backend.services;
using Backend.Validatiors.Login;
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
            // Arrange
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

            // Act
            var result = await _authService.LoginAsync(loginBody);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("token", result.Token);
        }

        [Fact]
        public async Task LoginAsync_ReturnsFailure_WhenUserNotFound()
        {
            // Arrange
            var loginBody = new LoginBody { Email = "test@example.com", Password = "password" };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(loginBody.Email))
                .ReturnsAsync((User)null);

            // Act
            var result = await _authService.LoginAsync(loginBody);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Invalid email or password", result.Message);
        }

        [Fact]
        public async Task LoginAsync_ReturnsFailure_WhenPasswordIsInvalid()
        {
            // Arrange
            var loginBody = new LoginBody { Email = "test@example.com", Password = "password" };

            var user = new User
            {
                Credentials = new Credentials { Email = loginBody.Email, PasswordHash = "hashedpassword" }
            };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(loginBody.Email))
                .ReturnsAsync(user);

            _passwordValidationMock.Setup(pv => pv.ValidatePassword(loginBody.Password, user.Credentials.PasswordHash))
                .Returns(false);

            // Act
            var result = await _authService.LoginAsync(loginBody);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Invalid email or password", result.Message);
        }

        [Fact]
        public async Task RegisterAsync_ReturnsSuccess_WhenUserIsRegisteredSuccessfully()
        {
            // Arrange
            var registerBody = new RegisterUserBody
            {
                Email = "newuser@example.com",
                Password = "password",
                IsClient = true
            };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(registerBody.Email))
                .ReturnsAsync((User)null);

            _jwtTokenGeneratorMock.Setup(jwt => jwt.GenerateToken(It.IsAny<User>()))
                .Returns("token");

            // Act
            var result = await _authService.RegisterAsync(registerBody);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("token", result.Token);
        }

        [Fact]
        public async Task RegisterAsync_ReturnsFailure_WhenUserAlreadyExists()
        {
            // Arrange
            var registerBody = new RegisterUserBody
            {
                Email = "existinguser@example.com",
                Password = "password",
                IsClient = true
            };

            var existingUser = new User
            {
                Credentials = new Credentials { Email = registerBody.Email }
            };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(registerBody.Email))
                .ReturnsAsync(existingUser); 

            // Act
            var result = await _authService.RegisterAsync(registerBody);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("User already exists", result.Message);
        }

        [Fact]
        public async Task DeleteUserAsync_ReturnsSuccess_WhenUserIsDeleted()
        {
            // Arrange
            var userId = 1;

            _userRepositoryMock.Setup(repo => repo.DeleteUserAsync(userId))
                .ReturnsAsync(true);

            // Act
            var result = await _authService.DeleteUserAsync(userId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("User deleted successfully", result.Message);
        }

        [Fact]
        public async Task DeleteUserAsync_ReturnsFailure_WhenUserNotFound()
        {
            // Arrange
            var userId = 1;

            _userRepositoryMock.Setup(repo => repo.DeleteUserAsync(userId))
                .ReturnsAsync(false);

            // Act
            var result = await _authService.DeleteUserAsync(userId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("User not found", result.Message);
        }
    }
}