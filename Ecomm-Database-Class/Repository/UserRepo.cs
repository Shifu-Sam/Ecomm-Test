using Ecomm_Database_Class.Data;
using Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecomm_Database_Class.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context = context;
        }

        // Create a new user
        public async Task CreateUserAsync(User user)
        {
            user.Password = HashPassword(user.Password);
            user.Role = "user"; // Default role for new users
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Read (Get all users)
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Read (Get user by ID)
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        // Update user details
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Delete a user
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        private string HashPassword(string password)
        {
            User user = new User();
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, password);
            return hashedPassword;
        }
    }
}
