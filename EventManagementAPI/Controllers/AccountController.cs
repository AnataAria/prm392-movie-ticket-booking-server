using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Service;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await _accountService.GetById(id);
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _accountService.GetAll();
            return Ok(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Account account)
        {
            var createdAccount = await _accountService.Add(account);
            return CreatedAtAction(nameof(GetById), new { id = createdAccount.Id }, createdAccount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Account account)
        {
            var existingAccount = await _accountService.GetById(id);
            if (existingAccount == null) return NotFound();

            account.Id = id;
            await _accountService.Update(account);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingAccount = await _accountService.GetById(id);
            if (existingAccount == null) return NotFound();

            await _accountService.Delete(id);
            return NoContent();
        }

        [HttpGet("GetAllName")]
        public async Task<IActionResult> GetAllName()
        {
            var accounts = await _accountService.GetAllName();
            return Ok(accounts);
        }

        [HttpPost("MinusDebt")]
        public async Task<IActionResult> MinusDebt([FromQuery] int? quantity, [FromQuery] int? prize, [FromQuery] double? discount, [FromBody] Account account)
        {
            if (quantity == null || prize == null || account == null) return BadRequest();

            await _accountService.MinusDebt(quantity, prize, discount, account);
            return NoContent();
        }

        [HttpPost("ValidateUser")]
        public async Task<IActionResult> GetSystemAccountByEmailAndPassword([FromQuery] string email, [FromQuery] string password)
        {
            var account = await _accountService.GetSystemAccountByEmailAndPassword(email, password);
            if (account == null) return Unauthorized(new { message = "Invalid email or password." });

            return Ok(account);
        }
    }
}
