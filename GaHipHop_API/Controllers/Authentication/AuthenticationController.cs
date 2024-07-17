using CoreApiResponse;
using GaHipHop_Model.DTO.Request;
using GaHipHop_Service.Interfaces;
using GaHipHop_Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GaHipHop_API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _authenticationService.AuthorizeUser(loginRequest);
            if (result.Token != null)
            {
                return CustomResult("Login successful.", new { result.Token, LoginResponse = result.loginResponse });
            }
            else
            {
                return CustomResult("Invalid email or password.", HttpStatusCode.Unauthorized);
            }
        }
    }
}
