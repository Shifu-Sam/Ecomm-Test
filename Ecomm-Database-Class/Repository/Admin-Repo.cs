using Ecomm_Database_Class.JwtAuth;
using Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static Ecomm_Database_Class.Data.AppDbContext;

namespace Ecomm_Database_Class.Repository
{
    public class Admin_Repo : IAdmin_Repo
    {
        public int InsertAdmin(AdminTable1 admin)
        {

            string query = "INSERT INTO AdminTable1 (Name, Email, Password ,Role) VALUES (@Name,@Email, @Password, @Role)";
            nameValuePairList parameters = new nameValuePairList
                {
                    new nameValuePair("Name", admin.Name),
                    new nameValuePair("Email",admin.Email),
                    new nameValuePair("Password",admin.Password),
                    new nameValuePair("Role", admin.Role)
                };

            DB1 db = new DB1();
            return db.InsertUpdateOrDelete(query, parameters);
        }

        

        public AdminTable1 GetAdminById(int adminId)
        {
            string query = "SELECT * FROM AdminTable1 WHERE AdminID = @AdminID";
            nameValuePairList parameters = new nameValuePairList
                {
                    new nameValuePair("AdminID", adminId)
                };

            DB1 db = new DB1();
            DataTable dataTable = db.FillAndReturnDataSet(query, parameters);

            return ConvertDataTableToAdmin(dataTable);
        }

        public AdminTable1 GetAdminByEmail(string email)
        {
            string query = "SELECT * FROM AdminTable1 WHERE Email = @email";
            nameValuePairList parameters = new nameValuePairList
            {
                new nameValuePair("Email",email)
            };
            DB1 db = new DB1();
            DataTable dataTable = db.FillAndReturnDataSet(query, parameters);
            return ConvertDataTableToAdmin(dataTable);
        }

        public int UpdateAdmin(AdminTable1 admin)
        {
            string query = "UPDATE AdminTable1 SET Name = @Name, Role =@Role WHERE AdminID =@id";
            nameValuePairList parameters = new nameValuePairList
                {
                    new nameValuePair("@Name", admin.Name),
                    new nameValuePair("@Role", admin.Role),
                    new nameValuePair("@id", admin.AdminID)

                };

            DB1 db = new DB1();
            return db.InsertUpdateOrDelete(query, parameters);
        }

        public int DeleteAdmin(int adminId)
        {
            string query = "DELETE FROM AdminTable1 WHERE AdminID = @AdminID";
            nameValuePairList parameters = new nameValuePairList
                {
                    new nameValuePair("AdminID", adminId)
                };

            DB1 db = new DB1();
            return db.InsertUpdateOrDelete(query, parameters);
        }

        private AdminTable1 ConvertDataTableToAdmin(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0)
                return null;

            DataRow row = dataTable.Rows[0];
            AdminTable1 admin = new AdminTable1
            {
                AdminID = Convert.ToInt32(row["AdminID"]),
                Name = row["Name"].ToString(),
                Email = row["Email"].ToString(),
                Password = row["Password"].ToString(),
                Role = row["Role"].ToString()
            };

            return admin;
        }
    }
}
