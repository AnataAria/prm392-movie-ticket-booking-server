using BusinessObjects;
using BusinessObjects.Dtos.Schema_Response;
using BusinessObjects.Dtos.Ticket;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using System.Net.Sockets;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("user/api/2024-11-11/transactiontypes")]
    [ApiController]
    public class TransactionTypeController(ITransactionTypeService transactionTypeService) : ControllerBase
    {
        private readonly ITransactionTypeService _transactionTypeService = transactionTypeService;

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<TransactionType>>> GetTransactionTypeById(int id)
        {
            try
            {
                var transactionType = await _transactionTypeService.GetById(id);
                if (transactionType == null)
                {
                    return NotFound(new ResponseModel<TransactionType>
                    {
                        Success = false,
                        Error = "Role not found",
                        ErrorCode = 404
                    });
                }
                return Ok(new ResponseModel<TransactionType>
                {
                    Success = true,
                    Data = transactionType
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<TicketDto> { Success = false, Error = ex.Message, ErrorCode = 500 });
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<TransactionType>>> GetAllTransactionTypes()
        {
            try
            {
                var transactionTypes = await _transactionTypeService.GetAll();
                return Ok(new ResponseModel<IEnumerable<TransactionType>>
                {
                    Success = true,
                    Data = transactionTypes
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<IEnumerable<TransactionType>>
                {
                    Success = false,
                    Error = ex.Message,
                    ErrorCode = 500
                });
            }
        }

        [HttpPost]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> AddTransactionType([FromBody] TransactionType transactionType)
        {
            var result = await _transactionTypeService.Add(transactionType);
            return CreatedAtAction(nameof(AddTransactionType), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> UpdateTransactionType(int id, [FromBody] TransactionType transactionType)
        {
            var existingTransactionType = await _transactionTypeService.GetById(id);
            if (existingTransactionType == null) return NotFound();

            transactionType.Id = id;
            await _transactionTypeService.Update(transactionType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> DeleteTransactionType(int id)
        {
            var existingTransactionType = await _transactionTypeService.GetById(id);
            if (existingTransactionType == null) return NotFound();

            await _transactionTypeService.Delete(id);
            return NoContent();
        }
    }
}
