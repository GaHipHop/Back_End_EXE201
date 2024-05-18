using CoreApiResponse;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Respone;
using GaHipHop_Repository.Entity;
using GaHipHop_Service.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GaHipHop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _adminService.AuthorizeUser(loginRequest);
            if (result.Token != null)
            {
                return CustomResult("Login successful.",new { Token = result.Token, LoginResponse = result.loginResponse });
            }
            else
            {
                return CustomResult("Invalid email or password.", HttpStatusCode.Unauthorized);
            }
        }

        [HttpGet]
        public IActionResult GetAllAdmin()
        {
            var admin = _adminService.GetAllAdmin();
            return CustomResult("Data load Successful",admin);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminById(long id)
        {
            try
            {
                var admin = await _adminService.GetAdminById(id);

                return CustomResult("Create admin successful",admin);
            }
            catch (Exception ex)
            {
                return CustomResult("Not found admin.", HttpStatusCode.NotFound);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminRequest adminRequest)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }

            var result = await _adminService.CreateAdmin(adminRequest);

            if (!result.Status)
            {
                return CustomResult("Create fail.", new { userName = result.UserName }, HttpStatusCode.Conflict);
            }

            return CustomResult("Create Successful", result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(long id, [FromBody] AdminRequest adminRequest)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return CustomResult(ModelState,HttpStatusCode.BadRequest);
            }

            try
            {
                var result = await _adminService.UpdateAdmin(id, adminRequest);
                return CustomResult("Update Successful",result);
            }
            catch (Exception ex)
            {
                return CustomResult("Update Admin Fail", HttpStatusCode.BadRequest);
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
                    return CustomResult("Delete Successful.");
                }
                else
                {
                    return CustomResult("Not found admin.", HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                return CustomResult("Delete Fail.", HttpStatusCode.BadRequest);
            }
        }


    }
}
