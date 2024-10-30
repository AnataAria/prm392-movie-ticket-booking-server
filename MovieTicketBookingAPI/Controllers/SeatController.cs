using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Service;

namespace MovieTicketBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatController(ISeatService seatService) : ControllerBase
    {
        private readonly ISeatService _seatService = seatService;
        [HttpGet("GetAvailableSeatsByShowtimeId/{showtimeId}")]
        public async Task<IActionResult> GetAvailableSeatsByShowtimeId(int showtimeId)
        {
            var seats = await _seatService.GetAvailableSeatsByShowtimeId(showtimeId);
            return Ok(seats);
        }
    }
}
