using Ecomm_Database_Class.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductImageController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("{productId}/upload")]
        public async Task<IActionResult> UploadProductImage(int productId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var filePath = Path.Combine("wwwroot/images", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return NotFound("Product not found.");

            product.ImageUrl = filePath;
            await _context.SaveChangesAsync();

            return Ok(new { product.Id, product.ImageUrl });
        }

        [HttpDelete("{productId}/delete")]
        public async Task<IActionResult> DeleteProductImage(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return NotFound("Product not found.");

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var filePath = product.ImageUrl;
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                product.ImageUrl = null;
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
