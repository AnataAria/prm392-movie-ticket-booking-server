using AutoMapper;
using BusinessObjects.Dtos.Auth;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using BusinessObjects.Dtos.Schema_Response;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<ActionResult<ResponseModel<AuthResponseDto>>> Login(LoginDto loginDto)
        {
            try
            {
                var response = await _authService.Login(loginDto);
                return Ok(new ResponseModel<AuthResponseDto>()
                {
                    Data = response,
                    Error = null,
                    ErrorCode = 200,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new ResponseModel<AuthResponseDto>()
                {
                    Data = null,
                    Error = "User or Password Is Not Correct",
                    Success = false,
                    ErrorCode = 401
                });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseModel<Account>>> Register(RegisterDto registerDto)
        {
            try
            {
                var newAccount = await _authService.Register(registerDto);
                return Ok(new ResponseModel<Account>()
                {
                    Data = newAccount,
                    Error = null,
                    Success = true,
                    ErrorCode = 200
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<AuthResponseDto>()
                {
                    Data = null,
                    Error = ex.Message,
                    Success = false,
                    ErrorCode = 500
                });
            }

        }

        [HttpGet("who-am-i"), Authorize]
        public async Task<ActionResult<ResponseModel<Account>>> WhoAmI()
        {
            try
            {
                var account = await _authService.GetUserByClaims(HttpContext.User);
                return Ok(new ResponseModel<Account>()
                {
                    Data = account,
                    Error = null,
                    Success = true,
                    ErrorCode = 200
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<Account>()
                {
                    Data = null,
                    Error = ex.Message,
                    Success = true,
                    ErrorCode = 200
                });
            }

        }
    }
}
