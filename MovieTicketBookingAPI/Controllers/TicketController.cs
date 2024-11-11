using BusinessObjects;
using BusinessObjects.Dtos.Count;
using BusinessObjects.Dtos.Schema_Response;
using BusinessObjects.Dtos.Ticket;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using System.Data;
using System.Net.Sockets;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("user/api/2024-11-11/tickets")]
    [ApiController]
    public class TicketController(ITicketService ticketService) : ControllerBase
    {
        private readonly ITicketService _ticketService = ticketService;

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<TicketDto>>> GetById(int id)
        {
            try
            {
                var ticket = await _ticketService.GetByIdInclude(id);
                if (ticket == null)
                {
                    return NotFound(new ResponseModel<TicketDto>
                    {
                        Success = false,
                        Error = "Ticket not found",
                        ErrorCode = 404
                    });
                }
                var ticketDto = new TicketDto
                {
                    Id = ticket.Id,
                    SeatID = ticket.SeatID,
                    SeatName = ticket.Seat.SeatNumber ?? "null",
                    MovieName = ticket.Movie.Name ?? "null",
                    ShowDateTime = ticket.Showtime.ShowDateTime,
                    Price = ticket.Price,
                    Status = ticket.Status,
                    Quantity = ticket.Quantity,
                };
                return Ok(new ResponseModel<TicketDto>
                {
                    Success = true,
                    Data = ticketDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<TicketDto> { Success = false, Error = ex.Message, ErrorCode = 500 });
            }
        }

        [HttpGet("movieid/{movieId}")]
        public async Task<ActionResult<ResponseModel<IEnumerable<TicketDto>>>> GetTicketsByMovieId(int movieId)
        {
            try
            {
                var tickets = await _ticketService.GetByMovieIdInclude(movieId);
                var ticketResponses = tickets.Select(ticket => new TicketDto
                {
                    Id = ticket.Id,
                    SeatID = ticket.SeatID,
                    SeatName = ticket.Seat.SeatNumber ?? "null",
                    MovieName = ticket.Movie.Name ?? "null",
                    ShowDateTime = ticket.Showtime.ShowDateTime,
                    Price = ticket.Price,
                    Status = ticket.Status,
                    Quantity = ticket.Quantity,
                });
                return Ok(new ResponseModel<IEnumerable<TicketDto>>
                {
                    Success = true,
                    Data = ticketResponses
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<IEnumerable<TicketDto>>
                {
                    Success = false,
                    Error = ex.Message,
                    ErrorCode = 500
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<TicketDto>>> GetAll()
        {
            try
            {
                var tickets = await _ticketService.GetAllIncludeAsync();
                var ticketResponses = tickets.Select(ticket => new TicketDto
                {
                    Id = ticket.Id,
                    SeatID = ticket.SeatID,
                    SeatName = ticket.Seat.SeatNumber ?? "null",
                    MovieName = ticket.Movie.Name ?? "null",
                    ShowDateTime = ticket.Showtime.ShowDateTime,
                    Price = ticket.Price,
                    Status = ticket.Status,
                    Quantity = ticket.Quantity,
                });
                return Ok(new ResponseModel<IEnumerable<TicketDto>>
                {
                    Success = true,
                    Data = ticketResponses
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<IEnumerable<TicketDto>>
                {
                    Success = false,
                    Error = ex.Message,
                    ErrorCode = 500
                });
            }
        }

        //[HttpPost("CountPeopleInMovie")]
        //public async Task<ActionResult<ResponseModel<CountRemainingTickets>>> CountQuantityPeopleJoinEvent([FromBody] Movie eventName)
        //{
        //    try
        //    {
        //        var result = await _ticketService.CountQuantityPeopleJoinEvent(eventName);
        //        return Ok(new { remainingTickets = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new ResponseModel<IEnumerable<CountRemainingTickets>>
        //        {
        //            Success = false,
        //            Error = ex.Message,
        //            ErrorCode = 500
        //        });
        //    }
        //}

        [HttpPost]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> AddTicket([FromBody] Ticket ticket)
        {
            var result = await _ticketService.Add(ticket);
            return CreatedAtAction(nameof(AddTicket), new { id = result.Id }, result);
        }

        [HttpPut("ticket/{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> UpdateNewTicket(int id, [FromBody] Ticket ticket)
        {
            var existingTicket = await _ticketService.GetById(id);
            if (existingTicket == null) return NotFound();

            ticket.Id = id;
            await _ticketService.UpdateNewTicket(ticket);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> UpdateTicket(int id, [FromBody] Ticket ticket)
        {
            var existingTicket = await _ticketService.GetById(id);
            if (existingTicket == null) return NotFound();

            ticket.Id = id;
            await _ticketService.Update(ticket);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var existingTicket = await _ticketService.GetById(id);
            if (existingTicket == null) return NotFound();

            await _ticketService.Delete(id);
            return NoContent();
        }
    }
}
