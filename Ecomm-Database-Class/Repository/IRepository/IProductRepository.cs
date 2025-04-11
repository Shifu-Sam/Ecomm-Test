using Ecomm_Database_Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Repository.IRepository
{
    public interface IProductRepository
    {
        
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetAllAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<Product?> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> GetBestSellingProductsAsync(int count);

    }
}
