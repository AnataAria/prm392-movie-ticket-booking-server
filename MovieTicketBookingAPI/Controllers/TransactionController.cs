using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using BusinessObjects;
using Services.Service;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("user/api/2024-11-11/transactions")]
    [ApiController]
    public class TransactionController(ITransactionService transactionService) : ControllerBase
    {
        private readonly ITransactionService _transactionService = transactionService;

        [HttpGet("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetById(id);
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }

        [HttpGet]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAll();
            return Ok(transactions);
        }

        [HttpPost]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> AddTransaction([FromBody] Transaction transaction)
        {
            var result = await _transactionService.Add(transaction);
            return CreatedAtAction(nameof(AddTransaction), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] Transaction transaction)
        {
            var existingTransaction = await _transactionService.GetById(id);
            if (existingTransaction == null) return NotFound();

            transaction.Id = id;
            await _transactionService.Update(transaction);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var existingTransaction = await _transactionService.GetById(id);
            if (existingTransaction == null) return NotFound();

            await _transactionService.Delete(id);
            return NoContent();
        }
    }
}
