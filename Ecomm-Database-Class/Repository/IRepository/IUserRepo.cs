using Ecomm_Database_Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Repository.IRepository
{
    public interface IUserRepo
    {
        // Create a new user
        Task CreateUserAsync(User user);

        // Read (Get all users)
        Task<List<User>> GetAllUsersAsync();

        // Read (Get user by ID)
        Task<User> GetUserByIdAsync(int id);

        // Update user details
        Task UpdateUserAsync(User user);

        // Delete a user
        Task DeleteUserAsync(int id);
    }
}
