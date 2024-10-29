﻿using AutoMapper;
using BusinessObjects.Dtos.Auth;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var response = await _authService.Login(loginDto);
                return Ok(response);
            } catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
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
