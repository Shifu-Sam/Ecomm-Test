using Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartOperations _cartOperations;

        public CartController(ICartOperations cartOperations)
        {
            _cartOperations = cartOperations;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            try
            {
                List<Cart> carts = await Task.Run(() => _cartOperations.GetAllCarts());
                return Ok(carts);
            }
            catch (Exception)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCartById(int id)
        {
            try
            {
                Cart cart = await _cartOperations.GetCartByIdAsync(id);
                if (cart == null)
                {
                    return NotFound($"Shopping cart with id {id} not found.");
                }
                return Ok(cart);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] Cart cart)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _cartOperations.AddCartAsync(cart);
                    return CreatedAtAction(nameof(GetCartById), new { id = cart.CartItemID }, cart);
                }
                catch (Exception ex)
                {
                    // Log the exception (not shown here for brevity)
                    return StatusCode(500, "Internal server error");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] Cart cart)
        {
            if (cart == null || cart.CartItemID != id)
            {
                return BadRequest("Cart data is invalid.");
            }

            try
            {
                await _cartOperations.UpdateCartAsync(cart);
                return Ok(new { message = "Cart updated successfully!" });
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            try
            {
                await _cartOperations.DeleteCartAsync(id);
                return Ok(new { message = "Cart deleted successfully!" });
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode(500, "Internal server error");
            }
        }
    }
}