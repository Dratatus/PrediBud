using Backend.Data.Models.Users;

namespace Backend.services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
