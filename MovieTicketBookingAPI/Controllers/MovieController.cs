using BusinessObjects;
using BusinessObjects.Dtos.Movie;
using BusinessObjects.Dtos.Schema_Response;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Service;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("user/api/2024-11-11/movies")]
    [ApiController]
    public class MovieController(IMovieService movieService) : ControllerBase
    {
        private readonly IMovieService _movieService = movieService;

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<MovieDto>>> GetById(int id)
        {
            try
            {
                var movie = await _movieService.GetById(id);
                if (movie == null)
                {
                    return NotFound(new ResponseModel<MovieDto>
                    {
                        Success = false,
                        Error = "Movie not found",
                        ErrorCode = 404
                    });
                }

                var movieDto = new MovieDto
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    DateStart = movie.DateStart ?? DateOnly.MinValue,
                    DateEnd = movie.DateEnd ?? DateOnly.MinValue,
                    Image = movie.Image,
                    Status = movie.Status ?? 0,
                    DirectorName = movie.DirectorName,
                    Description = movie.Description,
                    Showtime = movie.Tickets.Select(ticket => ticket.Showtime?.ShowDateTime.ToString("g")).ToList()
                };

                return Ok(new ResponseModel<MovieDto>
                {
                    Data = movieDto,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<MovieDto>
                {
                    Success = false,
                    Error = "An unexpected error occurred",
                    ErrorCode = 500
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<MovieDto>>>> GetAll()
        {
            try
            {
                var movies = await _movieService.GetAll();
                if (movies == null || !movies.Any())
                {
                    return NotFound(new ResponseModel<IEnumerable<MovieDto>>
                    {
                        Success = false,
                        Error = "No movies found",
                        ErrorCode = 404
                    });
                }

                var movieDtos = movies.Select(movie => new MovieDto
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    DateStart = movie.DateStart ?? DateOnly.MinValue,
                    DateEnd = movie.DateEnd ?? DateOnly.MinValue,
                    Image = movie.Image,
                    Status = movie.Status ?? 0,
                    DirectorName = movie.DirectorName,
                    Description = movie.Description,
                    Showtime = movie.Tickets.Select(ticket => ticket.Showtime?.ShowDateTime.ToString("g")).ToList()
                }).ToList();

                return Ok(new ResponseModel<IEnumerable<MovieDto>>
                {
                    Data = movieDtos,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<IEnumerable<MovieDto>>
                {
                    Success = false,
                    Error = "An unexpected error occurred",
                    ErrorCode = 500
                });
            }
        }

        [HttpPost]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> Create([FromBody] Movie eventObj)
        {
            var createdEvent = await _movieService.Add(eventObj);
            return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id }, createdEvent);
        }

        [HttpPut("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> Update(int id, [FromBody] Movie eventObj)
        {
            var existingEvent = await _movieService.GetById(id);
            if (existingEvent == null) return NotFound();

            eventObj.Id = id;
            await _movieService.Update(eventObj);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingEvent = await _movieService.GetById(id);
            if (existingEvent == null) return NotFound();

            await _movieService.Delete(id);
            return NoContent();
        }

        //[HttpGet("ListAllInclude")]
        //public async Task<IActionResult> GetAllInclude()
        //{
        //    var events = await _eventService.GetAllInclude();
        //    return Ok(events);
        //}

        //[HttpGet("ListAllIncludeType")]
        //public async Task<IActionResult> GetAllIncludeType()
        //{
        //    var events = await _movieService.GetAllIncludeType();
        //    return Ok(events);
        //}
    }
}
