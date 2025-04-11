using Ecomm_Database_Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Repository.IRepository
{
    public interface ISubCategoryRepository
    {
        Task<IEnumerable<SubCategory>> GetAllAsync();
        Task<SubCategory?> GetAllAsync(int id);
        Task<SubCategory> AddAsync(SubCategory subCategory);
        Task<SubCategory?> UpdateAsync(SubCategory subCategory);
        Task<bool> DeleteAsync(int id);
    }
}
