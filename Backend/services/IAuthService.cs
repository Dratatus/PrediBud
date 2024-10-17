using Backend.DTO.Auth;
using Backend.DTO;

namespace Backend.services
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(LoginBody request);
        Task<AuthResult> RegisterAsync(RegisterUserBody request);
        Task<AuthResult> DeleteUserAsync(int userId);
    }
}
