using Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _context;

        public UserController(IUserRepo context)
        {
            _context = context;
        }

        // Create User
        [HttpPost("Register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                await _context.CreateUserAsync(user);
                return Ok(new { message = "User created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"An error occurred while creating the user: {ex.Message}" });
            }
        }

        // Get All Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _context.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"An error occurred while fetching users: {ex.Message}" });
            }
        }

        // Get User by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _context.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"An error occurred while fetching the user: {ex.Message}" });
            }
        }

        // Update User
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                if (id != user.UserId)
                {
                    return BadRequest(new { message = "User ID mismatch." });
                }
                await _context.UpdateUserAsync(user);
                return Ok(new { message = "User updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"An error occurred while updating the user: {ex.Message}" });
            }
        }

        // Delete User
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _context.DeleteUserAsync(id);
                return Ok(new { message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"An error occurred while deleting the user: {ex.Message}" });
            }
        }
    }
}
