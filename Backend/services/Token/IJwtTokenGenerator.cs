using Backend.Data.Models.Users;

namespace Backend.services.Token
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
