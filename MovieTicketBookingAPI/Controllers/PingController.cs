using Microsoft.AspNetCore.Mvc;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet("CheckServer")]
        public IActionResult GetPingInfor() => Ok("PING CC DMMM");
    }
}
