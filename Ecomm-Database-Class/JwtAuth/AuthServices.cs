using Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecomm_Database_Class.JwtAuth
{

    public class AuthServices : IAuthServices
    {

        private readonly IAdmin_Repo _adminRepo;
        private readonly IConfiguration _configuration;
        private readonly IUserRepo _userRepo;

        public AuthServices(IConfiguration configuration, IAdmin_Repo adminRepo , IUserRepo userRepo)
        {
            _configuration = configuration;
            _adminRepo = adminRepo;
            _userRepo = userRepo;
        }


         string connectionString = "Data Source=localhost;Initial Catalog=ECommerce;Integrated Security=True;TrustServerCertificate=true";


        public async Task<AdminTable1?> AdminRegister(AdminTable1 adminTable)
        {
            var adminEmail = adminTable.Email;

            // Check if admin with the same email already exists
            AdminTable1 existingAdmin = _adminRepo.GetAdminByEmail(adminEmail);

            if (existingAdmin != null)
            {
                return null; // Return null if admin already exists
            }

            // Create a new AdminTable1 object and populate its properties
            var newAdmin = new AdminTable1();

            // Hash the password and assign it to the admin object
            string hashPassword = new PasswordHasher<AdminTable1>().HashPassword(newAdmin, adminTable.Password);

            newAdmin.Password = hashPassword;
            newAdmin.Name = adminTable.Name;
            newAdmin.Email = adminTable.Email;
            //newAdmin.Role = adminTable.Role;


            // ADO.NET code to insert the admin into the database

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO AdminTable1 (Name, Email, Password, Role) VALUES (@Name, @Email, @Password, @Role)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", newAdmin.Name);
                    command.Parameters.AddWithValue("@Email", newAdmin.Email);
                    command.Parameters.AddWithValue("@Password", newAdmin.Password);
                    command.Parameters.AddWithValue("@Role", "admin");

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }

            return newAdmin;
        }

        
        #region Create JwtToken for admin
        
        public string CreateToken(AdminTable1 admin)
        {
            var claim = new List<Claim> {
                new Claim(ClaimTypes.Name, admin.Name),
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim(ClaimTypes.NameIdentifier, admin.AdminID.ToString()) ,
                new Claim(ClaimTypes.Role,admin.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token"))
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:audience"),
                claims: claim,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        #endregion

        #region create token for user

        public string CreateToken(User user)
        {
            var claim = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()) ,
                new Claim(ClaimTypes.Role,user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token"))
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:audience"),
                claims: claim,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }


        #endregion

        #region Create token Identity
        public string CreateToken(IdentityUser user, List<string> roles)
        {
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token"))
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:audience"),
                claims: claim,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        } 
        #endregion



    }
}


