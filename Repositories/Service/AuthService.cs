using BusinessObjects.Dtos.Auth;
using BusinessObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DataAccessLayers.UnitOfWork;

namespace Services.Service
{
    public class AuthService(IConfiguration configuration, IAccountService accountService, IUnitOfWork unitOfWork) : GenericService<Account>(unitOfWork), IAuthService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IAccountService _accountService = accountService;

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            var account = await _accountService.GetSystemAccountByEmailAndPassword(loginDto.Email, loginDto.Password);

            if (account == null || !VerifyPassword(loginDto.Password, account.Password ?? ""))
            {
                throw new UnauthorizedAccessException("Wrong email or password.");
            }

            var token = CreateToken(account);
            return new AuthResponseDto { Token = token };
        }

        public async Task<Account> Register(RegisterDto registerDto)
        {
            var existingAccount = await _accountService.GetSystemAccountByEmailAndPassword(registerDto.Email, registerDto.Password);

            if (existingAccount != null)
            {
                throw new Exception("Email is already registered.");
            }

            var hashedPassword = HashPassword(registerDto.Password);
            var newAccount = new Account
            {
                Email = registerDto.Email,
                Password = hashedPassword,
                Name = registerDto.FullName,
                RoleId = registerDto.RoleId,
                Wallet = 0
            };

            await _accountService.Add(newAccount);
            return newAccount;
        }

        private string CreateToken(Account account)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, account.Email!),
                new(ClaimTypes.Role, account.Role!.Name!),
                new("uid", account.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(int.Parse(_configuration["JwtSettings:DurationInDays"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string hashedPassword = HashPassword(enteredPassword);
            return hashedPassword.Equals(storedHash);
        }

        public async Task<Account> GetUserByClaims(ClaimsPrincipal claims)
        {
            var userId = claims.FindFirst(c => c.Type == "uid")?.Value;

            if (userId == null)
            {
                throw new Exception("User not found.");
            }

            var account = await _accountService.GetById(int.Parse(userId)); // Assuming GetById returns Account

            if (account == null)
            {
                throw new Exception("User not found.");
            }

            return account;
        }
    }
}
