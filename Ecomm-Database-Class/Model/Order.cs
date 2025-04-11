
using System.ComponentModel.DataAnnotations;


namespace Ecomm_Database_Class.Model
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "UserID is required")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "TotalPrice is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalPrice must be greater than 0")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "ShippingAddress is required")]
        [StringLength(500, ErrorMessage = "ShippingAddress can't be longer than 500 characters")]
        public string ShippingAddress { get; set; }

        
        public string OrderStatus { get; set; }

        public string PaymentStatus { get; set; }
    }
}
