using Backend.Data.Models.Common;
using Backend.Data.Models.Credidentials;
using Backend.Data.Models.Users;
using Backend.DTO;
using Backend.DTO.Auth;
using Backend.Repositories;
using Backend.Validation;

namespace Backend.services
{
    public class AuthService: IAuthService
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
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return new AuthResult { Success = false, Message = "Invalid email or password" };

            var validPassword = _passwordValidator.ValidatePassword(request.Password, user.Credentials.PasswordHash);
            if (!validPassword)
                return new AuthResult { Success = false, Message = "Invalid email or password" };

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResult { Success = true, Token = token };
        }

        public async Task<AuthResult> RegisterAsync(RegisterUserBody request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new AuthResult { Success = false, Message = "User already exists" };
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User user;

            if (request.IsClient)
            {
                user = new Client
                {
                    Credentials = new Credentials { Email = request.Email, PasswordHash = hashedPassword },
                    ContactDetails = new ContactDetails { Name = request.Name, Phone = request.Phone },
                    Address = new Address {City = request.Address.City, PostCode = request.Address.PostCode, StreetName = request.Address.StreetName }
                    
                };
            }
            else
            {
                user = new Worker
                {
                    Credentials = new Credentials { Email = request.Email, PasswordHash = hashedPassword },
                    ContactDetails = new ContactDetails { Name = request.Name, Phone = request.Phone },
                    Position = request.Position
                };
            }

            await _userRepository.AddUserAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResult { Success = true, Token = token };
        }
        public async Task<AuthResult> DeleteUserAsync(int userId)
        {
            var success = await _userRepository.DeleteUserAsync(userId);
            if (!success)
            {
                return new AuthResult { Success = false, Message = "User not found" };
            }

            return new AuthResult { Success = true, Message = "User deleted successfully" };
        }
    }
}

