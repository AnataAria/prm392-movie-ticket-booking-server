using BusinessObjects;
using BusinessObjects.Dtos.Auth;
using BusinessObjects.Dtos.Schema_Response;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Service;
using System.Net;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService _accountService = accountService;

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<AccountResponseBasic>>> GetById(int id)
        {
            try
            {
                var account = await _accountService.GetById(id);
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
                if (account == null)
                    return NotFound(new ResponseModel<AccountResponseBasic> { Success = false, Error = "Account not found", ErrorCode = 404 });

                return Ok(new ResponseModel<AccountResponseBasic> { Success = true, Data = accountResponse });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<AccountResponseBasic> { Success = false, Error = ex.Message, ErrorCode = 500 });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var accounts = await _accountService.GetAll();
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
                    Role = account.Role.Name,
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

        [HttpDelete("{id}")]
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
                    Role = existingAccount.Role.Name,
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

        [HttpPost("ValidateUser")]
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
