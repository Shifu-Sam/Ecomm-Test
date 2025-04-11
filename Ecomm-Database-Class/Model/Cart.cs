using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Model
{
    public class Cart
    {   
        [Key]
        public int CartItemID { get; set; }

        public int ProductID { get; set; }  

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }  

        [Required(ErrorMessage = "Total Price is required.")]
        public decimal TotalPrice { get; set; } 

    }
}
