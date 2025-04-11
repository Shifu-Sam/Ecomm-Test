using Ecomm_Database_Class.JwtAuth;
using Ecomm_Database_Class.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecomm_Database_Class.Repository.IRepository
{
    public interface IAdmin_Repo
    {
        int InsertAdmin(AdminTable1 admin);
        AdminTable1 GetAdminById(int adminId);
        int UpdateAdmin(AdminTable1 admin);
        int DeleteAdmin(int adminId);

        AdminTable1 GetAdminByEmail(string email);
    }
}
