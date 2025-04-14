using Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepo _context;

        public AddressController(IAddressRepo addressRepo)
        {
            _context = addressRepo;
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] Address address)
        {
            if (address == null)
                return BadRequest("Invalid address data.");

            await _context.AddAddressAsync(address);
            return Ok(new { message = "Address added successfully." });
        }

        [Authorize(Roles = "user")]
        [HttpGet("{addressId}")]
        public async Task<IActionResult> GetAddressById(int addressId)
        {
            var address = await _context.GetAddressByIdAsync(addressId);

            if (address == null)
                return NotFound("Address not found.");

            return Ok(address);
        }

        [Authorize(Roles = "user")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAddressesByUserId(int userId)
        {
            var addresses = await _context.GetAddressesByUserIdAsync(userId);

            if (addresses == null || addresses.Count == 0)
                return NotFound("No addresses found for the user.");

            return Ok(addresses);
        }

        [Authorize(Roles = "user")]
        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress(int addressId, [FromBody] Address address)
        {
            if (address == null || address.AddressId != addressId)
                return BadRequest("Invalid address data.");

            var existingAddress = await _context.GetAddressByIdAsync(addressId);
            if (existingAddress == null)
                return NotFound("Address not found.");

            try
            {
                
                _context.DetachEntity(existingAddress);

                // Update the address
                await _context.UpdateAddressAsync(address);
                return Ok(new { message = "Address updated successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = "Error updating the address: " + ex.Message });
            }
        }

        [Authorize(Roles = "user")]
        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            var existingAddress = await _context.GetAddressByIdAsync(addressId);
            if (existingAddress == null)
                return NotFound("Address not found.");

            await _context.DeleteAddressAsync(addressId);
            return Ok(new { message = "Address deleted successfully." });
        }
    }
}
