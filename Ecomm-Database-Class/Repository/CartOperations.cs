using Ecomm_Database_Class.Data;
using Ecomm_Database_Class.Model;
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

        public Cart GetCartById(int id)
        {
            return _context.CartItems.FirstOrDefault(c => c.CartItemID == id) ?? throw new InvalidOperationException("Cart not found");
        }

        public void AddCart(Cart cart)
        {
            _context.CartItems.Add(cart);
            _context.SaveChanges();
        }

        public void UpdateCart(Cart cart)
        {
            _context.CartItems.Update(cart);
            _context.SaveChanges();
        }

        public void DeleteCart(int id)
        {
            var cart = _context.CartItems.FirstOrDefault(c => c.CartItemID == id);
            if (cart != null)
            {
                _context.CartItems.Remove(cart);
                _context.SaveChanges();
            }
        }

        public async Task<Cart> GetCartByIdAsync(int id)
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
    }
}
