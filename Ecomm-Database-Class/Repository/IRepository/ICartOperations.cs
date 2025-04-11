using Ecomm_Database_Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Repository.IRepository
{
    public interface ICartOperations 
    {
        List<Cart> GetAllCarts();
        Task<Cart> GetCartByIdAsync(int id);
        Task AddCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(int id);
    }
}
