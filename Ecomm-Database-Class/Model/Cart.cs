using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Model
{
    namespace Ecomm_Database_Class.Model
    {
        public class Cart
        {
            [Key]
            public int CartItemID { get; set; }

            [ForeignKey("Product")]
            public int ProductId { get; set; }
            [ForeignKey("User")]
            public int UserId { get; set; }

            [Required(ErrorMessage = "Quantity is required.")]
            public int Quantity { get; set; }

            public bool IsActive { get; set; } = true;
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public DateTime UpdatedAt { get; set; }

        }
    }
}
