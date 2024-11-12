using Microsoft.AspNetCore.Mvc;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/2024-11-11/pings")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet("server")]
        public IActionResult GetPingInfo() => Ok(new {
            message = "PING"
        });
    }
}
