using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace vtbbook.Core.Web.Controllers
{
    [ApiController]
    public class PingConroller : ControllerBase
    {
        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok("It's a ping!");
        }

        [Authorize]
        [Route("auth/ping")]
        public IActionResult AuthPing()
        {
            return Ok($"It's a auth ping {User.Identity.Name}!");
        }
    }
}
