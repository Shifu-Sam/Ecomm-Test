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
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AppDbContext _dbcontext;

        public SubCategoryRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<SubCategory>> GetAllAsync() =>
            await _dbcontext.SubCategories.ToListAsync();

        public async Task<SubCategory?> GetAllAsync(int id) =>
            await _dbcontext.SubCategories.FindAsync(id);

        public async Task<SubCategory> AddAsync(SubCategory subCategory)
        {
            _dbcontext.SubCategories.Add(subCategory);
            await _dbcontext.SaveChangesAsync();
            return subCategory;
        }

        public async Task<SubCategory?> UpdateAsync(SubCategory subCategory)
        {
            var existing = await _dbcontext.SubCategories.FindAsync(subCategory.SubCategoryId);
            if (existing == null) return null;

            _dbcontext.Entry(existing).CurrentValues.SetValues(subCategory);
            await _dbcontext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subCategory = await _dbcontext.SubCategories.FindAsync(id);
            if (subCategory == null) return false;

            _dbcontext.SubCategories.Remove(subCategory);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
