using BusinessObjects;
using BusinessObjects.Dtos.Schema_Response;
using BusinessObjects.Dtos.Ticket;
using BusinessObjects.Dtos.TransactionHistory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Services.Interface;
using Services.Service;
using System.Collections.Generic;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryController(ITransactionHistoryService transactionHistoryService, IAuthService authService) : ControllerBase
    {
        private readonly ITransactionHistoryService _transactionHistoryService = transactionHistoryService;
        private readonly IAuthService _authService = authService;

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetTransactionHistoryById(int id)
        //{
        //    var transactionHistory = await _transactionHistoryService.GetById(id);
        //    if (transactionHistory == null) return NotFound();
        //    return Ok(transactionHistory);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAllTransactionHistories()
        //{
        //    var transactionHistories = await _transactionHistoryService.GetAll();
        //    return Ok(transactionHistories);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddTransactionHistory([FromBody] TransactionHistory transactionHistory)
        //{
        //    var result = await _transactionHistoryService.Add(transactionHistory);
        //    return CreatedAtAction(nameof(AddTransactionHistory), new { id = result.Id }, result);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTransactionHistory(int id, [FromBody] TransactionHistory transactionHistory)
        //{
        //    var existingTransactionHistory = await _transactionHistoryService.GetById(id);
        //    if (existingTransactionHistory == null) return NotFound();

        //    transactionHistory.Id = id;
        //    await _transactionHistoryService.Update(transactionHistory);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTransactionHistory(int id)
        //{
        //    var existingTransactionHistory = await _transactionHistoryService.GetById(id);
        //    if (existingTransactionHistory == null) return NotFound();

        //    await _transactionHistoryService.Delete(id);
        //    return NoContent();
        //}

        //[HttpGet("/Account/{accountId}")]
        //public async Task<IActionResult> GetTransactionHistoryByAccountId(int accountId)
        //{
        //    var transactionHistory = await _transactionHistoryService.GetTransactionHistoryByAccountId(accountId);
        //    if (transactionHistory == null) return NotFound();
        //    return Ok(transactionHistory);
        //}

        [HttpGet("ListAll/account")]
        [Authorize]
        public async Task<ActionResult<ResponseModel<IEnumerable<TransactionHistoryDto>>>> GetAllTransactionHistoryByAccountId()
        {
            var account = await _authService.GetUserByClaims(HttpContext.User);
            try
            {
                var transactionHistories = await _transactionHistoryService.GetAllTransactionHistoryByAccountId(account.Id);
                if (transactionHistories == null || !transactionHistories.Any())
                {
                    return Ok(new ResponseModel<IEnumerable<TransactionHistoryDto>>
                    {
                        Success = true,
                        Data = transactionHistories
                    });
                }

                return Ok(new ResponseModel<IEnumerable<TransactionHistoryDto>>
                {
                    Success = true,
                    Data = transactionHistories
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<TransactionHistoryDto> { Success = false, Error = ex.Message, ErrorCode = 500 });
            }
        }
    }
}