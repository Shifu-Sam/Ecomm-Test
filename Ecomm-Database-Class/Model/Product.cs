using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Model
{
    public class Product
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Brand is required")]
    [StringLength(50, ErrorMessage = "Brand can't be longer than 50 characters")]
    public string Brand { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters")]
    public string Description { get; set; } = string.Empty;
    [Url(ErrorMessage = "Invalid URL format")]
    public string? ImageUrl { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number")]
    public int Quantity { get; set; }
    [Required(ErrorMessage = "CategoryId is required")]

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    
    [Required(ErrorMessage = "SubCategoryId is required")]
    [ForeignKey("SubCategory")]
    public int SubCategoryId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "SoldCount must be a non-negative number")]
    public int? SoldCount { get; internal set; }
    
    }
}
