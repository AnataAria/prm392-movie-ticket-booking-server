using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Service;

namespace MovieTicketBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowTimeController(IShowTimeService showTimeService) : ControllerBase
    {
        private readonly IShowTimeService _showTimeService = showTimeService;

        [HttpGet("GetShowtimesByMovieId/{movieId}")]
        public async Task<IActionResult> GetShowtimesByMovieId(int movieId)
        {
            var showtimes = await _showTimeService.GetShowtimesByMovieId(movieId);
            return Ok(showtimes);
        }
    }
}
