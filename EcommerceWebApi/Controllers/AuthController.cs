using Ecomm_Database_Class.Model.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IAuthServices _authServices;
        //private readonly IAdmin_Repo _adminRepo;
        //private readonly IUserRepo _userRepo;

        //public AuthController(IAuthServices authServices, IAdmin_Repo adminRepo, IUserRepo userRepo)
        //{
        //    _authServices = authServices;
        //    _adminRepo = adminRepo;
        //    _userRepo = userRepo;
        //}

        //[HttpPost("RegisterAdmin")]
        //public async Task<ActionResult<AdminTable1>> Register(AdminTable1 adminTable)
        //{
        //    var admin = await _authServices.AdminRegister(adminTable);

        //    if (admin == null)
        //    {
        //        return BadRequest("User already exists");
        //    }
        //    return Ok(admin);
        //}

        //[HttpPost("adminLogin")]
        //public ActionResult<string> Login(AdminDto adminDto)
        //{
        //    AdminTable1 admin = _adminRepo.GetAdminByEmail(adminDto.Email);
        //    if (admin == null)
        //    {
        //        return BadRequest("Admin does not exists");
        //    }

        //    if(new PasswordHasher<AdminTable1>().VerifyHashedPassword(admin, admin.Password, adminDto.Password) 
        //        == PasswordVerificationResult.Failed)
        //    {
        //        return BadRequest("Wrong password");
        //    }

        //    string token = _authServices.CreateToken(admin);

        //    return Ok(token);
        //}

        //[HttpPost("userLogin")]
        //public async Task<ActionResult<string>> Login(UserDto userDto)
        //{
        //    User user = await _userRepo.GetUserByEmailAsync(userDto.Email);

        //    if (user == null)
        //        return BadRequest("User does not exist");

        //    if (string.IsNullOrEmpty(user.Password))
        //        return BadRequest("User password is not set");

        //    if (new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, userDto.Password)
        //        == PasswordVerificationResult.Failed)
        //    {
        //        return BadRequest("Wrong password");
        //    }

        //    string token = _authServices.CreateToken(user);
        //    return Ok(token);
        //}

        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.Email,
                EmailConfirmed = true
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors.Select(e => e.Description));
            }

            if (!string.IsNullOrEmpty(registerRequestDto.Roles))
            {
                identityResult = await userManager.AddToRoleAsync(identityUser, registerRequestDto.Roles);

                if (!identityResult.Succeeded)
                {
                    return BadRequest(identityResult.Errors.Select(e => e.Description));
                }
            }

            return Ok("User registered successfully");
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Email);

            if (user != null)
            {
                var checkPasswordResult =  await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    // Create jwt token
                    return Ok("LoggedIn");
                }
            }

            return BadRequest("User does exist");
        }



    }
}
