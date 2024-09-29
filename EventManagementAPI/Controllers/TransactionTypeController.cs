using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypeController : ControllerBase
    {
        private readonly ITransactionTypeService _transactionTypeService;

        public TransactionTypeController(ITransactionTypeService transactionTypeService)
        {
            _transactionTypeService = transactionTypeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionTypeById(int id)
        {
            var transactionType = await _transactionTypeService.GetById(id);
            if (transactionType == null) return NotFound();
            return Ok(transactionType);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactionTypes()
        {
            var transactionTypes = await _transactionTypeService.GetAll();
            return Ok(transactionTypes);
        }

        [HttpPost]
        public async Task<IActionResult> AddTransactionType([FromBody] TransactionType transactionType)
        {
            var result = await _transactionTypeService.Add(transactionType);
            return CreatedAtAction(nameof(AddTransactionType), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransactionType(int id, [FromBody] TransactionType transactionType)
        {
            var existingTransactionType = await _transactionTypeService.GetById(id);
            if (existingTransactionType == null) return NotFound();

            transactionType.Id = id;
            await _transactionTypeService.Update(transactionType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionType(int id)
        {
            var existingTransactionType = await _transactionTypeService.GetById(id);
            if (existingTransactionType == null) return NotFound();

            await _transactionTypeService.Delete(id);
            return NoContent();
        }
    }
}
