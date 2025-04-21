using Ecomm_Database_Class.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Database_Class.JwtAuth
{
    public interface IAuthServices
    {
        Task<AdminTable1?> AdminRegister(AdminTable1 adminTable);
        string CreateToken(AdminTable1 admin);
        string CreateToken(User user);
        string CreateToken(IdentityUser user, List<string> roles);

    }
}
