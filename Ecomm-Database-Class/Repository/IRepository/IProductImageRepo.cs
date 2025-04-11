using Ecomm_Database_Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Repository.IRepository
{
    public interface IProductImageRepo
    {
        Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId);
        Task<ProductImage> GetProductImageAsync(int id);
        Task AddProductImageAsync(ProductImage productImage);
        Task DeleteProductImageAsync(int id);
    }
}
