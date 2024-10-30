using BusinessObjects.Dtos.Schema_Response;
using BusinessObjects.Dtos.ShowTime;
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
        public async Task<ActionResult<ResponseModel<List<ShowtimeDto>>>> GetShowtimesByMovieId(int movieId)
        {
            var response = new ResponseModel<List<ShowtimeDto>>();
            try
            {
                var showtimes = await _showTimeService.GetShowtimesByMovieId(movieId);

                if (showtimes == null || !showtimes.Any())
                {
                    response.Success = false;
                    response.Error = "No showtimes found for the specified movie.";
                    response.ErrorCode = 404;
                    return NotFound(response);
                }

                response.Data = showtimes;
                response.Success = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.Message;
                response.ErrorCode = 500;
                return StatusCode(500, response);
            }
        }
    }
}
