using Ecomm_Database_Class.Data;
using Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Model.Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 


namespace Ecomm_Database_Class.Repository
{
    public class CartOperations : ICartOperations
    {
        private readonly AppDbContext _context;

        public CartOperations(AppDbContext context)
        {
            _context = context;
        }

        public List<Cart> GetAllCarts()
        {
            return _context.CartItems.ToList();
        }

        public async Task<Cart> GetAllCarts(int id)
        {
            return await _context.CartItems.FirstOrDefaultAsync(c => c.CartItemID == id) ?? throw new InvalidOperationException("Cart not found");
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _context.CartItems.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            _context.CartItems.Update(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(int id)
        {
            var cart = await _context.CartItems.FirstOrDefaultAsync(c => c.CartItemID == id);
            if (cart != null)
            {
                _context.CartItems.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        //get all cart items by UserId
        public async Task<List<Cart>> GetCartsByUserIdAsync(int userId)
        {
            return await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        // Get all cart items by ProductId
        public async Task<List<Cart>> GetCartsByProductIdAsync(int productId)
        {
            return await _context.CartItems
                .Where(c => c.ProductId == productId)
                .ToListAsync();
        }
    }

}
