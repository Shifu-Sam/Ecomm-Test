using Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryRepository _repo;

        public SubCategoryController(ISubCategoryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subCategories = await _repo.GetAllAsync();
            return Ok(subCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var subCategory = await _repo.GetAllAsync(id);
            return subCategory == null ? NotFound() : Ok(subCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubCategory subCategory)
        {
            var created = await _repo.AddAsync(subCategory);
            return CreatedAtAction(nameof(GetById), new { id = created.SubCategoryId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SubCategory subCategory)
        {
            if (id != subCategory.SubCategoryId) return BadRequest();
            var updated = await _repo.UpdateAsync(subCategory);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
