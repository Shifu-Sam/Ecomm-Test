using Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Repository.IRepository;
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

        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] Address address)
        {
            if (address == null)
                return BadRequest("Invalid address data.");

            await _context.AddAddressAsync(address);
            return Ok(new { message = "Address added successfully." });
        }

        [HttpGet("{addressId}")]
        public async Task<IActionResult> GetAddressById(int addressId)
        {
            var address = await _context.GetAddressByIdAsync(addressId);

            if (address == null)
                return NotFound("Address not found.");

            return Ok(address);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAddressesByUserId(int userId)
        {
            var addresses = await _context.GetAddressesByUserIdAsync(userId);

            if (addresses == null || addresses.Count == 0)
                return NotFound("No addresses found for the user.");

            return Ok(addresses);
        }

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
                // Detach the existing entity to avoid tracking conflicts

                //400 Undocumented Error: response status is 400 Response body Download { "message": "Error updating the address: The instance of entity type 'Address' cannot be tracked because another instance with the same key value for {'AddressId'} is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values." }
                //To overcome above error i have added this line
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
