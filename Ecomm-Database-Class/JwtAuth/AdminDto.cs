using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.JwtAuth
{
    public class AdminDto
    {

        //public string Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }
        //public string Role { get; set; } = "admin";
    }
}
