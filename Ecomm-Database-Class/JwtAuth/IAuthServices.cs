using Ecomm_Database_Class.Model;
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

    }
}
