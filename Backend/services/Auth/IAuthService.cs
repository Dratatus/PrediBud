using Backend.DTO.Auth;
using Backend.DTO;

namespace Backend.services.Auth
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(LoginBody request);
        Task<AuthResult> RegisterAsync(RegisterUserBody request);
        Task DeleteUserAsync(int userId);
    }
}
