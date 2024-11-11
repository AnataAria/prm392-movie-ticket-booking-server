using BusinessObjects;
using BusinessObjects.Dtos.Account;
using BusinessObjects.Dtos.Auth;
using BusinessObjects.Dtos.Schema_Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Service;
using System.Net;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("user/api/2024-11-11/accounts")]
    [ApiController]
    public class AccountController(IAccountService accountService, IAuthService authService) : ControllerBase
    {
        private readonly IAccountService _accountService = accountService;
        private readonly IAuthService _authService = authService;

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<AccountResponseBasic>>> GetById(int id)
        {
            try
            {
                var account = await _accountService.GetAccountByIdIncludeAsync(id);
                if (account == null)
                    return NotFound(new ResponseModel<AccountResponseBasic> { Success = false, Error = "Account not found", ErrorCode = 404 });
                var accountResponse = new AccountResponseBasic
                {
                    Id = account.Id,
                    Name = account.Name,
                    Address = account.Address,
                    Phone = account.Phone,
                    Role = account.Role.Name,
                    Status = account.Status,
                    Email = account.Email,
                    Wallet = account.Wallet
                };

                return Ok(new ResponseModel<AccountResponseBasic> { Success = true, Data = accountResponse });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<AccountResponseBasic> { Success = false, Error = ex.Message, ErrorCode = 500 });
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<AccountResponseBasic>>>> GetAll()
        {
            try
            {
                var accounts = await _accountService.GetAllIncludeAsync();
                var accountResponses = accounts.Select(account => new AccountResponseBasic
                {
                    Id = account.Id,
                    Name = account.Name,
                    Address = account.Address,
                    Phone = account.Phone,
                    Role = account.Role.Name,
                    Status = account.Status,
                    Email = account.Email,
                    Wallet = account.Wallet
                });
                return Ok(new ResponseModel<IEnumerable<AccountResponseBasic>> { Success = true, Data = accountResponses });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<IEnumerable<AccountResponseBasic>> { Success = false, Error = ex.Message, ErrorCode = 500 });
            }
        }

        [HttpPost]
        [Tags("CRUD Server Only")]
        public async Task<ActionResult<ResponseModel<AccountResponseBasic>>> Create([FromBody] Account account)
        {
            if (account == null)
                return BadRequest(new ResponseModel<AccountResponseBasic> { Success = false, Error = "Invalid account data", ErrorCode = 400 });
            try
            {
                var createdAccount = await _accountService.Add(account);
                var accountResponse = new AccountResponseBasic
                {
                    Id = account.Id,
                    Name = account.Name,
                    Address = account.Address,
                    Phone = account.Phone,
                    Role = "Anonymous",
                    Status = account.Status,
                    Email = account.Email,
                    Wallet = account.Wallet
                };
                return CreatedAtAction(nameof(GetById), new { id = createdAccount.Id }, new ResponseModel<AccountResponseBasic> { Success = true, Data = accountResponse });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<AccountResponseBasic> { Success = false, Error = ex.Message, ErrorCode = 500 });
            }
        }

        [HttpPut("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<ActionResult<ResponseModel<AccountResponseBasic>>> Update(int id, [FromBody] AccountResponseBasic account)
        {
            if (account == null)
                return BadRequest(new ResponseModel<AccountResponseBasic> { Success = false, Error = "Invalid account data", ErrorCode = 400 });

            try
            {
                var existingAccount = await _accountService.GetById(id);
                if (existingAccount == null)
                    return NotFound(new ResponseModel<AccountResponseBasic> { Success = false, Error = "Account not found", ErrorCode = 404 });
                bool isUpdated = false;

                if (existingAccount.Name != account.Name)
                {
                    existingAccount.Name = account.Name;
                    isUpdated = true;
                }
                if (existingAccount.Address != account.Address)
                {
                    existingAccount.Address = account.Address;
                    isUpdated = true;
                }
                if (existingAccount.Phone != account.Phone)
                {
                    existingAccount.Phone = account.Phone;
                    isUpdated = true;
                }
                if (existingAccount.Status != account.Status)
                {
                    existingAccount.Status = account.Status;
                    isUpdated = true;
                }
                if (existingAccount.Email != account.Email)
                {
                    existingAccount.Email = account.Email;
                    isUpdated = true;
                }
                if (existingAccount.Wallet != account.Wallet)
                {
                    existingAccount.Wallet = account.Wallet;
                    isUpdated = true;
                }

                if (isUpdated)
                {
                    await _accountService.Update(existingAccount);
                    return Ok(new ResponseModel<AccountResponseBasic> { Success = true, Data = account });
                }
                return Ok(new ResponseModel<AccountResponseBasic> { Success = false, Data = account, Error = "No changes detected" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<AccountResponseBasic> { Success = false, Error = ex.Message, ErrorCode = 500 });
            }
        }

        [HttpPut("profile/{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseModel<UserDto>>> UpdateUser([FromBody] UserDto account)
        {
            var accountUser = await _authService.GetUserByClaims(HttpContext.User);
            if (account == null)
                return BadRequest(new ResponseModel<UserDto> { Success = false, Error = "Invalid account data", ErrorCode = 400 });

            try
            {
                var existingAccount = await _accountService.GetById(accountUser.Id);
                if (existingAccount == null)
                    return NotFound(new ResponseModel<UserDto> { Success = false, Error = "Account not found", ErrorCode = 404 });

                if (existingAccount.Name != account.Name)
                {
                    existingAccount.Name = account.Name;
                }
                if (existingAccount.Address != account.Address)
                {
                    existingAccount.Address = account.Address;
                }
                if (existingAccount.Phone != account.Phone)
                {
                    existingAccount.Phone = account.Phone;
                }

                await _accountService.Update(existingAccount);
                return Ok(new ResponseModel<UserDto> { Success = true, Data = account });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<UserDto> { Success = false, Error = ex.Message, ErrorCode = 500 });
            }
        }

        [HttpPut("wallet")]
        [Authorize]
        public async Task<ActionResult<ResponseModel<UserUpdateWalletDto>>> UpdateWallet([FromQuery] double wallet)
        {
            var accountUser = await _authService.GetUserByClaims(HttpContext.User);
            if (wallet < 0)
                return BadRequest(new ResponseModel<UserUpdateWalletDto> { Success = false, Error = "Invalid number", ErrorCode = 400 });
            try
            {
                var existingAccount = await _accountService.GetById(accountUser.Id);
                if (existingAccount == null)
                    return NotFound(new ResponseModel<UserUpdateWalletDto> { Success = false, Error = "Account not found", ErrorCode = 404 });
                existingAccount.Wallet += wallet;

                await _accountService.Update(existingAccount);
                var updatedUserDto = new UserUpdateWalletDto
                {
                    Id = existingAccount.Id,
                    Name = existingAccount.Name,
                    Wallet = existingAccount.Wallet
                };
                return Ok(new ResponseModel<UserUpdateWalletDto> { Success = true, Data = updatedUserDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<UserUpdateWalletDto> { Success = false, Error = ex.Message, ErrorCode = 500 });
            }
        }

        [HttpDelete("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<ActionResult<ResponseModel<AccountResponseBasic>>> Delete(int id)
        {
            try
            {
                var existingAccount = await _accountService.GetById(id);
                if (existingAccount == null)
                    return NotFound(new ResponseModel<AccountResponseBasic> { Success = false, Error = "Account not found", ErrorCode = 404 });

                var accountResponse = new AccountResponseBasic
                {
                    Id = existingAccount.Id,
                    Name = existingAccount.Name,
                    Address = existingAccount.Address,
                    Phone = existingAccount.Phone,
                    Role = "Anonymous",
                    Status = existingAccount.Status,
                    Email = existingAccount.Email,
                    Wallet = existingAccount.Wallet
                };

                await _accountService.Delete(id);
                return Ok(new ResponseModel<AccountResponseBasic> { Success = true, Data = accountResponse });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<AccountResponseBasic> { Success = false, Error = ex.Message, ErrorCode = 500 });
            }
        }

        //[HttpGet("GetAllName")]
        //public async Task<IActionResult> GetAllName()
        //{
        //    var accounts = await _accountService.GetAllName();
        //    return Ok(accounts);
        //}

        //[HttpPost("MinusDebt")]
        //public async Task<IActionResult> MinusDebt([FromQuery] int? quantity, [FromQuery] int? prize, [FromQuery] double? discount, [FromBody] Account account)
        //{
        //    if (quantity == null || prize == null || account == null) return BadRequest();

        //    await _accountService.MinusDebt(quantity, prize, discount, account);
        //    return NoContent();
        //}

        [HttpPost("validation")]
        public async Task<ActionResult<ResponseModel<AccountResponseBasic>>> GetSystemAccountByEmailAndPassword([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                var account = await _accountService.GetSystemAccountByEmailAndPassword(email, password);
                if (account == null)
                    return Unauthorized(new ResponseModel<string> { Success = false, Error = "Invalid email or password", ErrorCode = 401 });

                var accountResponse = new AccountResponseBasic
                {
                    Id = account.Id,
                    Name = account.Name,
                    Address = account.Address,
                    Phone = account.Phone,
                    Role = account.Role.Name,
                    Status = account.Status,
                    Email = account.Email,
                    Wallet = account.Wallet
                };

                return Ok(new ResponseModel<AccountResponseBasic> { Success = true, Data = accountResponse });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<string> { Success = false, Error = ex.Message, ErrorCode = 500 });
            }
        }
    }
}
