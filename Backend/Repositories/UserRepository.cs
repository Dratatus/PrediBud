using Backend.Data.Context;
using Backend.Data.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly PrediBudDBContext _context;

        public UserRepository(PrediBudDBContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.ID == userId);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Credentials.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasMaterialOrdersAsync(int userId)
        {
            return await _context.MaterialOrders.AnyAsync(mo => mo.UserId == userId);
        }

        public async Task<bool> HasConstructionOrdersAsync(int userId)
        {
            return await _context.ConstructionOrders.AnyAsync(co => co.ClientId == userId || co.WorkerId == userId);
        }

    }
}
