using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.Model
{
    public class AdminTable1
    {
        public int AdminID { get; set; }
        public  string Name { get; set; } 
        public  string Email { get; set; } 
        public  string Password { get; set; } 
        public string? Role { get; set; }   = "admin";


    }
}
