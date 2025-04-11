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
    public class ProductImageRepo : IProductImageRepo
    {
        private readonly AppDbContext _context;

        public ProductImageRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId)
        {
            return await _context.ProductImages
            .Where(pi => pi.ProductId == productId)
            .ToListAsync();
        }

        public async Task<ProductImage> GetProductImageAsync(int id)
        {
            return await _context.ProductImages.FindAsync(id);
        }

        public async Task AddProductImageAsync(ProductImage productImage)
        {
            await _context.ProductImages.AddAsync(productImage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductImageAsync(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage != null)
            {
                _context.ProductImages.Remove(productImage);
                await _context.SaveChangesAsync();
            }
        }
    }
}
