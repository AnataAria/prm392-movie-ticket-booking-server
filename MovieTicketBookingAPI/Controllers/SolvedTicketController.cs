using BusinessObjects;
using BusinessObjects.Dtos.SolvedTicket;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolvedTicketController(ISolvedTicketService solvedTicketService) : ControllerBase
    {
        private readonly ISolvedTicketService _solvedTicketService = solvedTicketService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSolvedTicketById(int id)
        {
            var solvedTicket = await _solvedTicketService.GetById(id);
            if (solvedTicket == null) return NotFound();

            return Ok(solvedTicket);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSolvedTickets()
        {
            var solvedTickets = await _solvedTicketService.GetAll();
            return Ok(solvedTickets);
        }

        [HttpPost]
        public async Task<IActionResult> AddSolvedTicket([FromBody] SolvedTicket solvedTicket)
        {
            var result = await _solvedTicketService.Add(solvedTicket);
            return CreatedAtAction(nameof(GetSolvedTicketById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSolvedTicket(int id, [FromBody] SolvedTicket solvedTicket)
        {
            var existingSolvedTicket = await _solvedTicketService.GetById(id);
            if (existingSolvedTicket == null) return NotFound();

            solvedTicket.Id = id;
            await _solvedTicketService.Update(solvedTicket);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolvedTicket(int id)
        {
            var existingSolvedTicket = await _solvedTicketService.GetById(id);
            if (existingSolvedTicket == null) return NotFound();

            await _solvedTicketService.Delete(id);
            return NoContent();
        }

        [HttpPost("PurchaseTickets")]
        public async Task<IActionResult> PurchaseTickets([FromBody] PurchaseTicketRequestDto request)
        {
            await _solvedTicketService.PurchaseTickets(request.Tickets, request.Account, request.Quantity);
            return Ok();
        }

        [HttpGet("Account/{accountId}")]
        public async Task<IActionResult> GetSolvedTicketsByAccountId(int accountId)
        {
            var solvedTickets = await _solvedTicketService.GetSolvedTicketsByAccountId(accountId);
            return Ok(solvedTickets);
        }

        [HttpGet("Check/{ticketId}")]
        public async Task<IActionResult> CheckSolvedTicket(int ticketId)
        {
            var result = await _solvedTicketService.CheckSolvedTicket(ticketId);
            return Ok(result);
        }
    }
}
