using AutoMapper;
using BusinessObjects.Dtos.Auth;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace EventManagementAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _authService.Login(loginDto);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var newAccount = await _authService.Register(registerDto);
            return Ok(newAccount);
        }

        [HttpGet("who-am-i"), Authorize]
        public async Task<IActionResult> WhoAmI()
        {
            var account = await _authService.GetUserByClaims(HttpContext.User);
            return Ok(account);
        }
    }
}
