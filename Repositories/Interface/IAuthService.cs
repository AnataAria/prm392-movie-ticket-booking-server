using BusinessObjects.Dtos.Auth;
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginDto loginDto);
        Task<Account> Register(RegisterDto registerDto);
        Task<Account> GetUserByClaims(ClaimsPrincipal claims);
    }
}