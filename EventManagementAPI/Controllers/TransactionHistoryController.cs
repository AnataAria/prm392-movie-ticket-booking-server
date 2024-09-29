using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Services.Interface;
using Services.Service;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly ITransactionHistoryService _transactionHistoryService;

        public TransactionHistoryController(ITransactionHistoryService transactionHistoryService)
        {
            _transactionHistoryService = transactionHistoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionHistoryById(int id)
        {
            var transactionHistory = await _transactionHistoryService.GetById(id);
            if (transactionHistory == null) return NotFound();
            return Ok(transactionHistory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactionHistories()
        {
            var transactionHistories = await _transactionHistoryService.GetAll();
            return Ok(transactionHistories);
        }

        [HttpPost]
        public async Task<IActionResult> AddTransactionHistory([FromBody] TransactionHistory transactionHistory)
        {
            var result = await _transactionHistoryService.Add(transactionHistory);
            return CreatedAtAction(nameof(AddTransactionHistory), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransactionHistory(int id, [FromBody] TransactionHistory transactionHistory)
        {
            var existingTransactionHistory = await _transactionHistoryService.GetById(id);
            if (existingTransactionHistory == null) return NotFound();

            transactionHistory.Id = id;
            await _transactionHistoryService.Update(transactionHistory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionHistory(int id)
        {
            var existingTransactionHistory = await _transactionHistoryService.GetById(id);
            if (existingTransactionHistory == null) return NotFound();

            await _transactionHistoryService.Delete(id);
            return NoContent();
        }

        [HttpGet("/Account/{accountId}")]
        public async Task<IActionResult> GetTransactionHistoryByAccountId(int accountId)
        {
            var transactionHistory = await _transactionHistoryService.GetTransactionHistoryByAccountId(accountId);
            if (transactionHistory == null) return NotFound();
            return Ok(transactionHistory);
        }

        [HttpGet("ListAll/account/{accountId}")]
        public async Task<IActionResult> GetAllTransactionHistoryByAccountId(int accountId)
        {
            var transactionHistories = await _transactionHistoryService.GetAllTransactionHistoryByAccountId(accountId);
            if (transactionHistories == null) return NotFound();
            return Ok(transactionHistories);
        }
    }
}
