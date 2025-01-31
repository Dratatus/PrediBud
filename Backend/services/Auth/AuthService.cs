using Backend.Data.Consts;
using Backend.Data.Models.Common;
using Backend.Data.Models.Credidentials;
using Backend.Data.Models.Users;
using Backend.DTO;
using Backend.DTO.Auth;
using Backend.Middlewares;
using Backend.Repositories;
using Backend.services.Token;
using Backend.Validatiors.DeleteUser;
using Backend.Validatiors.Login;

namespace Backend.services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordValidation _passwordValidator;

        public AuthService(IUserRepository userRepository, IPasswordValidation passwordValidator, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordValidator = passwordValidator;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResult> LoginAsync(LoginBody request)
        {
            LoginValidator.Validate(request);

            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                throw new ApiException(ErrorMessages.InvalidEmailOrPassword, StatusCodes.Status401Unauthorized);

            var validPassword = _passwordValidator.ValidatePassword(request.Password, user.Credentials.PasswordHash);

            if (!validPassword)
                throw new ApiException(ErrorMessages.InvalidEmailOrPassword, StatusCodes.Status401Unauthorized);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResult { Success = true, Token = token };
        }

        public async Task<AuthResult> RegisterAsync(RegisterUserBody request)
        {
            RegisterUserValidator.Validate(request);

            var existingUser = await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new ApiException(ErrorMessages.UserAlreadyExists, StatusCodes.Status400BadRequest);
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User user;

            if (request.IsClient)
            {
                user = new Client
                {
                    Credentials = new Credentials { Email = request.Email, PasswordHash = hashedPassword },
                    ContactDetails = new ContactDetails { Name = request.Name, Phone = request.Phone },
                    Address = new Address
                    {
                        City = request.Address.City,
                        PostCode = request.Address.PostCode,
                        StreetName = request.Address.StreetName
                    }
                };
            }
            else
            {
                user = new Worker
                {
                    Credentials = new Credentials { Email = request.Email, PasswordHash = hashedPassword },
                    ContactDetails = new ContactDetails { Name = request.Name, Phone = request.Phone },
                    Address = new Address
                    {
                        City = request.Address.City,
                        PostCode = request.Address.PostCode,
                        StreetName = request.Address.StreetName
                    },
                    Position = request.Position
                };
            }

            await _userRepository.AddUserAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResult { Success = true, Token = token };
        }

        public async Task DeleteUserAsync(int userId)
        {
            DeleteUserValidator.Validate(userId);

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new ApiException(ErrorMessages.UserNotFound, StatusCodes.Status404NotFound);
            }

            DeleteUserValidator.ValidateUserOrders(user, await _userRepository.HasMaterialOrdersAsync(userId), await _userRepository.HasConstructionOrdersAsync(userId));

            var success = await _userRepository.DeleteUserAsync(userId);
            if (!success)
            {
                throw new ApiException(ErrorMessages.FailedToDeleteUser, StatusCodes.Status500InternalServerError);
            }
        }
    }
}

