﻿using Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Model.Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            try
            {
                List<Cart> carts = _cartOperations.GetAllCarts();
                return Ok(carts);
            }
            catch (Exception)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Roles = "user")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCartById(int id)
        {
            try
            {
                Cart cart = await _cartOperations.GetAllCarts(id);
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

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] Cart cart)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _cartOperations.AddCartAsync(cart);
                    return Ok(cart);
                }
                catch (Exception ex)
                {
                    // Log the exception (not shown here for brevity)
                    return StatusCode(500, "Internal server error");
                }
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "user")]
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

        [Authorize(Roles = "user")]
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

        [Authorize("admin")]
        [HttpGet("userId/{userId:int}")]
        public async Task<IActionResult> GetAllCartsByUserId(int userId)
        {
            try
            {

                List<Cart> carts = await _cartOperations.GetCartsByUserIdAsync(userId);
                return Ok(carts);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode(500, "Internal server error");
            }

        }

        [Authorize("admin")]
        [HttpGet("productId/{productId:int}")]

        public async Task<IActionResult> GetAllCartsByProductId(int productId)
        {
            try
            {
                List<Cart> carts = await _cartOperations.GetCartsByProductIdAsync(productId);
                return Ok(carts);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
