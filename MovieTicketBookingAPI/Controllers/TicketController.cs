using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using System.Net.Sockets;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController(ITicketService ticketService) : ControllerBase
    {
        private readonly ITicketService _ticketService = ticketService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _ticketService.GetById(id);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await _ticketService.GetAll();
            return Ok(tickets);
        }

        [HttpGet("GetTicketsByEventId/{eventId}")]
        public async Task<IActionResult> GetTicketsByEventId(int eventId)
        {
            var tickets = await _ticketService.GetByMovieIdAsync(eventId);
            return Ok(tickets);
        }

        [HttpPost("CountPeopleInEvent")]
        public async Task<IActionResult> CountQuantityPeopleJoinEvent([FromBody] Movie eventName)
        {
            var result = await _ticketService.CountQuantityPeopleJoinEvent(eventName);
            return  Ok(new { remainingTickets = result });
        }

        [HttpPost]
        public async Task<IActionResult> AddTicket([FromBody] Ticket ticket)
        {
            var result = await _ticketService.Add(ticket);
            return CreatedAtAction(nameof(AddTicket), new { id = result.Id }, result);
        }

        [HttpPut("UpdateNewTicket/{id}")]
        public async Task<IActionResult> UpdateNewTicket(int id, [FromBody] Ticket ticket)
        {
            var existingTicket = await _ticketService.GetById(id);
            if (existingTicket == null) return NotFound();

            ticket.Id = id;
            await _ticketService.UpdateNewTicket(ticket);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, [FromBody] Ticket ticket)
        {
            var existingTicket = await _ticketService.GetById(id);
            if (existingTicket == null) return NotFound();

            ticket.Id = id;
            await _ticketService.Update(ticket);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var existingTicket = await _ticketService.GetById(id);
            if (existingTicket == null) return NotFound();

            await _ticketService.Delete(id);
            return NoContent();
        }
    }
}
