using CoreApiResponse;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Model.DTO.Response;
using GaHipHop_Repository.Entity;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools;

namespace GaHipHop_API.Controllers.Admin
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

        [HttpGet("getAllAdminByStatusTrue")]
        [Authorize(Roles = "Manager")]
        public IActionResult GetAllAdminByStatusTrue()
        {
            try
            {
                var admin = _adminService.GetAllAdminByStatusTrue();
                return CustomResult("Data load Successful", admin);
            }
            catch (CustomException.UnAuthorizedException ex)
            {
                return CustomResult("ahihi", HttpStatusCode.Unauthorized);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("getAllAdminByStatusFalse")]
        [Authorize(Roles = "Manager")]
        public IActionResult GetAllAdminByStatusFalse()
        {
            try
            {
                var admin = _adminService.GetAllAdminByStatusFalse();
                return CustomResult("Data load Successful", admin);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }

        [HttpGet("getAdminById/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAdminById(long id)
        {
            try
            {
                var admin = await _adminService.GetAdminById(id);

                return CustomResult("get admin successful", admin);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost("createAdmin")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminRequest adminRequest)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }
            try
            {
                var result = await _adminService.CreateAdmin(adminRequest);

                if (!result.Status)
                {
                    return CustomResult("Create fail.", new { userName = result.UserName }, HttpStatusCode.Conflict);
                }

                return CustomResult("Create Successful", result);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InvalidDataException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Conflict);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }

        }

        [HttpPatch("updateAdmin/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateAdmin(long id, [FromBody] AdminRequest adminRequest)
        {
            if (!ModelState.IsValid)
            {
                //return BadRequest(ModelState);
                return CustomResult(ModelState, HttpStatusCode.BadRequest);
            }

            try
            {
                var result = await _adminService.UpdateAdmin(id, adminRequest);
                return CustomResult("Update Successful", result);
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("deletetAdmin/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteAdmin(long id)
        {
            try
            {
               var result = await _adminService.DeleteAdmin(id);
               return CustomResult("Delete Successful.");
            }
            catch (CustomException.DataNotFoundException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
            catch (CustomException.ForbbidenException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.Forbidden);
            }
            catch (CustomException.InternalServerErrorException ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.InternalServerError);
            }
        }


    }
}
