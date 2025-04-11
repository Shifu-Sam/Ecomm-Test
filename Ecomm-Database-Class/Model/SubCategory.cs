using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Model
{
    public class SubCategory
    {
        [Key]
        public int SubCategoryId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "CategoryId is required")]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
    }
}
