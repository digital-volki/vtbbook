using Microsoft.AspNetCore.Mvc;
using vtbbook.Application.Domain;
using vtbbook.Models;

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


        [Route("ping")]
        [HttpPost]
        public IActionResult Ping([FromBody] PingModel pingModel)
        {
            return Json($"It's a ping! Message: `{pingModel.PingText}` - from {pingModel.PingSender}");
        }
    }
}
