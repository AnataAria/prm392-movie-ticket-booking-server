using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Service;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController(IMovieService movieService) : ControllerBase
    {
        private readonly IMovieService _movieService = movieService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var eventObj = await _movieService.GetById(id);
            if (eventObj == null) return NotFound();
            return Ok(eventObj);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _movieService.GetAll();
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Movie eventObj)
        {
            var createdEvent = await _movieService.Add(eventObj);
            return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id }, createdEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Movie eventObj)
        {
            var existingEvent = await _movieService.GetById(id);
            if (existingEvent == null) return NotFound();

            eventObj.Id = id;
            await _movieService.Update(eventObj);
            return NoContent();
        }

        [HttpDelete("{id}")]
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

        [HttpGet("ListAllIncludeType")]
        public async Task<IActionResult> GetAllIncludeType()
        {
            var events = await _movieService.GetAllIncludeType();
            return Ok(events);
        }
    }
}
