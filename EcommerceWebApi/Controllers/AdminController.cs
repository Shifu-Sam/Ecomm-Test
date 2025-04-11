using Microsoft.AspNetCore.Mvc;
using Ecomm_Database_Class.Model;
using Ecomm_Database_Class.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace Ecomm_Database_Class.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin_Repo _adminRepo;

        public AdminController(IAdmin_Repo adminRepo)
        {
            _adminRepo = adminRepo;
        }

        [HttpPost]
        [Route("InsertAdmin")]
        public IActionResult InsertAdmin([FromBody] AdminTable1 admin)
        {
            if (admin == null)
            {
                return BadRequest("Admin is null.");
            }

            int result = _adminRepo.InsertAdmin(admin);
            if (result > 0)
            {
                return Ok("Admin inserted successfully.");
            }
            else
            {
                return StatusCode(500, "An error occurred while inserting the admin.");
            }
        }

        [HttpGet]
        [Route("GetAdminById/{id}")]
        public IActionResult GetAdminById(int id)
        {
            AdminTable1 admin = _adminRepo.GetAdminById(id);
            if (admin == null)
            {
                return NotFound("Admin not found.");
            }

            return Ok(admin);
        }

        [HttpPut]
        [Route("UpdateAdmin")]
        public IActionResult UpdateAdmin([FromBody] AdminTable1 admin)
        {
            if (admin == null)
            {
                return BadRequest("Admin is null.");
            }

            int result = _adminRepo.UpdateAdmin(admin);
            if (result > 0)
            {
                return Ok("Admin updated successfully.");
            }
            else
            {
                return StatusCode(500, "An error occurred while updating the admin.");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("DeleteAdmin/{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            int result = _adminRepo.DeleteAdmin(id);
            if (result > 0)
            {
                return Ok("Admin deleted successfully.");
            }
            else
            {
                return StatusCode(500, "An error occurred while deleting the admin.");
            }
        }
    }

}
