using Backend.Data.Models.Users;

namespace Backend.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<User> GetUserByIdAsync(int userId);
    }
}
