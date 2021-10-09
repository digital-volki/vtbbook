using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace vtbbook.Controllers
{
    [ApiController]
    public class PingConroller : ControllerBase
    {
        public PingConroller()
        {

        }

        [Route("ping")]
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("It's a ping!");
        }

        [Authorize]
        [Route("auth/ping")]
        [HttpGet]
        public IActionResult AuthPing()
        {
            return Ok($"It's a auth ping {User.Identity.Name}!");
        }
    }
}
