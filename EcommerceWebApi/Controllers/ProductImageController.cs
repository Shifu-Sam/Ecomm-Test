using Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepo _productImageRepo;
        private readonly IProductRepository _productRepo;
        private readonly string _imageDirectory = "wwwroot/images";

        public ProductImageController(IProductImageRepo productImageRepo, IProductRepository productRepo)
        {
            _productImageRepo = productImageRepo;
            _productRepo = productRepo;
        }

        // GET: api/ProductImage/{productId}
        [HttpGet("{productId}")]
        public async Task<ActionResult<IEnumerable<ProductImage>>> GetProductImages(int productId)
        {
            var productImages = await _productImageRepo.GetProductImagesAsync(productId);
            return Ok(productImages);
        }

        // GET: api/ProductImage/{id}
        [HttpGet("image/{id}")]
        public async Task<ActionResult<ProductImage>> GetProductImage(int id)
        {
            var productImage = await _productImageRepo.GetProductImageAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }
            return Ok(productImage);
        }

        // POST: api/ProductImage/{productId}/upload
        [HttpPost("{productId}/upload")]
        public async Task<IActionResult> UploadProductImage(int productId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var filePath = Path.Combine(_imageDirectory, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var product = await _productRepo.GetAllAsync(productId);
            if (product == null)
                return NotFound("Product not found.");

            var productImage = new ProductImage
            {
                ImagePath = filePath,
                ProductId = productId
            };

            await _productImageRepo.AddProductImageAsync(productImage);

            // Update the Product's ImageUrl property
            product.ImageUrl = filePath;
            await _productRepo.UpdateAsync(product);

            return CreatedAtAction(nameof(GetProductImage), new { id = productImage.Id }, productImage);
        }

        // DELETE: api/ProductImage/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var productImage = await _productImageRepo.GetProductImageAsync(id);
            if (productImage == null)
                return NotFound("Product image not found.");

            if (System.IO.File.Exists(productImage.ImagePath))
            {
                System.IO.File.Delete(productImage.ImagePath);
            }

            await _productImageRepo.DeleteProductImageAsync(id);
            return NoContent();
        }
    }
}
