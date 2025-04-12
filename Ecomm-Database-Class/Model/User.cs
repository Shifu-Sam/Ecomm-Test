using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255)]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


        [Required(ErrorMessage = "Phone is required")]
        public string? Phone { get; set; }


        [Required(ErrorMessage = "Payment Details is required")]
        public string? PaymentDetails { get; set; }


        public string? Role { get; set; }
    }
}
