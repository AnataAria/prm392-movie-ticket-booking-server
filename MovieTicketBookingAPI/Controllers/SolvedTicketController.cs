    using BusinessObjects;
using BusinessObjects.Dtos.Schema_Response;
using BusinessObjects.Dtos.SolvedTicket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Service;
using System;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolvedTicketController(ISolvedTicketService solvedTicketService, IAuthService authService) : ControllerBase
    {
        private readonly ISolvedTicketService _solvedTicketService = solvedTicketService;
        private readonly IAuthService _authService = authService;

        [HttpGet("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> GetSolvedTicketById(int id)
        {
            var solvedTicket = await _solvedTicketService.GetById(id);
            if (solvedTicket == null) return NotFound();

            return Ok(solvedTicket);
        }

        [HttpGet]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> GetAllSolvedTickets()
        {
            var solvedTickets = await _solvedTicketService.GetAll();
            return Ok(solvedTickets);
        }

        [HttpPost]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> AddSolvedTicket([FromBody] SolvedTicket solvedTicket)
        {
            var result = await _solvedTicketService.Add(solvedTicket);
            return CreatedAtAction(nameof(GetSolvedTicketById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> UpdateSolvedTicket(int id, [FromBody] SolvedTicket solvedTicket)
        {
            var existingSolvedTicket = await _solvedTicketService.GetById(id);
            if (existingSolvedTicket == null) return NotFound();

            solvedTicket.Id = id;
            await _solvedTicketService.Update(solvedTicket);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> DeleteSolvedTicket(int id)
        {
            var existingSolvedTicket = await _solvedTicketService.GetById(id);
            if (existingSolvedTicket == null) return NotFound();

            await _solvedTicketService.Delete(id);
            return NoContent();
        }

        [HttpPost("PurchaseTickets")]
        [Authorize]
        public async Task<ActionResult<ResponseModel<PurchaseTicketResponseDto>>> PurchaseTickets([FromBody] PurchaseTicketRequestDto request)
        {
            if (request.ShowtimeId <= 0 || request.SeatIds == null || !request.SeatIds.Any())
            {
                return BadRequest(new ResponseModel<PurchaseTicketResponseDto>
                {
                    Success = false,
                    Data = null,
                    Error = "Invalid ShowtimeId or SeatIds.",
                    ErrorCode = 400
                });
            }
            try
            {
                var account = await _authService.GetUserByClaims(HttpContext.User);
                if (account == null)
                {
                    return NotFound(new ResponseModel<PurchaseTicketResponseDto>
                    {
                        Success = false,
                        Data = null,
                        Error = "Account not found.",
                        ErrorCode = 404
                    });
                }
                var response= await _solvedTicketService.PurchaseTickets(request.ShowtimeId, request.SeatIds, account);
                return Ok(new ResponseModel<PurchaseTicketResponseDto>
                {
                    Data = response,
                    Success = true
                });
            }
            catch (Exception ex) when (ex.Message.Contains("Showtime not found"))
            {
                return NotFound(new ResponseModel<PurchaseTicketResponseDto>
                {
                    Success = false,
                    Data = null,
                    Error = "Showtime not found",
                    ErrorCode = 404
                });
            }
            catch (Exception ex) when (ex.Message.Contains("Insufficient account balance"))
            {
                return BadRequest(new ResponseModel<PurchaseTicketResponseDto>
                {
                    Success = false,
                    Data = null,
                    Error = "Insufficient account balance",
                    ErrorCode = 400
                });
            }
            catch (Exception ex) when (ex.Message.Contains("Insufficient funds"))
            {
                return BadRequest(new ResponseModel<PurchaseTicketResponseDto>
                {
                    Success = false,
                    Data = null,
                    Error = "Insufficient funds",
                    ErrorCode = 400
                });
            }
            catch (Exception ex) when (ex.Message.Contains("Ticket for seat"))
            {
                return BadRequest(new ResponseModel<PurchaseTicketResponseDto>
                {
                    Success = false,
                    Data = null,
                    Error = ex.Message,
                    ErrorCode = 400
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<PurchaseTicketResponseDto>
                {
                    Success = false,
                    Error = ex.Message,
                    ErrorCode = 500
                });
            }
        }

        [HttpGet("Account/{accountId}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> GetSolvedTicketsByAccountId(int accountId)
        {
            var solvedTickets = await _solvedTicketService.GetSolvedTicketsByAccountId(accountId);
            return Ok(solvedTickets);
        }

        [HttpGet("Check/{ticketId}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> CheckSolvedTicket(int ticketId)
        {
            var result = await _solvedTicketService.CheckSolvedTicket(ticketId);
            return Ok(result);
        }
    }
}
