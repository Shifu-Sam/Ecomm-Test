using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Model
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ImagePath { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
