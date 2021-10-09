using Microsoft.AspNetCore.Mvc;

namespace vtbbook.Controllers
{
    [ApiController]
    public class PingConroller : VtbConrollerBase
    {
        [Route("ping")]
        [HttpGet]
        public IActionResult Ping()
        {
            return Json($"It's a ping!");
        }
    }
}
