using Ecomm_Database_Class.JwtAuth;
using Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly IAdmin_Repo _adminRepo;

        public AuthController(IAuthServices authServices, IAdmin_Repo adminRepo)
        {
            _authServices = authServices;
            _adminRepo = adminRepo;
        }

        [HttpPost("RegisterAdmin")]
        public async Task<ActionResult<AdminTable1>> Register(AdminTable1 adminTable)
        {
            var admin = await _authServices.AdminRegister(adminTable);

            if (admin == null)
            {
                return BadRequest("User already exists");
            }
            return Ok(admin);
        }

        [HttpPost("Login")]
        public ActionResult<string> Login(AdminDto adminDto)
        {
            AdminTable1 admin = _adminRepo.GetAdminByEmail(adminDto.Email);
            if (admin == null)
            {
                return BadRequest("Admin does not exists");
            }

            if(new PasswordHasher<AdminTable1>().VerifyHashedPassword(admin, admin.Password, adminDto.Password) 
                == PasswordVerificationResult.Failed)
            {
                return BadRequest("Wrong password");
            }

            string token = _authServices.CreateToken(admin);

            return Ok(token);
        }


    }
}
