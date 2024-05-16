using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Respone;
using GaHipHop_Repository.Entity;
using GaHipHop_Service.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GaHipHop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<AdminResponse>> GetAllAdmin()
        {
            var admin = _adminService.GetAllAdmin();
            return Ok(admin);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminResponse>> GetAdminById(long id)
        {
            try
            {
                var admin = await _adminService.GetAdminById(id);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<AdminResponse>> CreateAdmin([FromBody] AdminRequest adminRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _adminService.CreateAdmin(adminRequest);

            if (result == null)
            {
                return BadRequest("Failed to create admin.");
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AdminResponse>> UpdateAdmin(long id, [FromBody] AdminRequest adminRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _adminService.UpdateAdmin(id, adminRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(long id)
        {
            try
            {
                var result = await _adminService.DeleteAdmin(id);
                if (result)
                {
                    return Ok("Admin deleted successfully");
                }
                else
                {
                    return BadRequest("Failed to delete admin");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
