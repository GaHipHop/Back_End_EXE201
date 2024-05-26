using CoreApiResponse;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GaHipHop_API.Controllers.Kind
{
    [Route("api/[controller]")]
    [ApiController]
    public class KindController : BaseController
    {
        private readonly IKindService _kindService;

        public KindController(IKindService kindService)
        {
            _kindService = kindService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            try
            {
                var kindResponse = await _kindService.UploadImage(file);
                if (kindResponse == null)
                {
                    return StatusCode(500, "Error uploading image to Firebase.");
                }

                return Ok(new { DownloadUrl = kindResponse }); // Return download URL
            }
            catch (Exception ex)
            {
                // Log the Firebase exception (e.g., using a logging framework)
                return StatusCode(500, $"Firebase error: {ex.Message}");
            }
        }
    }
}
