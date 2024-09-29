using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using BusinessObjects;
using Services.Service;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetById(id);
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAll();
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] Transaction transaction)
        {
            var result = await _transactionService.Add(transaction);
            return CreatedAtAction(nameof(AddTransaction), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] Transaction transaction)
        {
            var existingTransaction = await _transactionService.GetById(id);
            if (existingTransaction == null) return NotFound();

            transaction.Id = id;
            await _transactionService.Update(transaction);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var existingTransaction = await _transactionService.GetById(id);
            if (existingTransaction == null) return NotFound();

            await _transactionService.Delete(id);
            return NoContent();
        }
    }
}
