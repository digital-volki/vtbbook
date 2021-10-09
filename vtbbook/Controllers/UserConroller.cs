using Microsoft.AspNetCore.Mvc;

namespace vtbbook.Controllers
{
    [ApiController]
    public class UserConroller : VtbConrollerBase
    {
        public UserConroller()
        {

        }

        [Route("auth/reg")]
        [HttpPost]
        public IActionResult Registration()
        {
            return Json("");
        }

        [Route("auth")]
        [HttpGet]
        public IActionResult Authorization()
        {
            return Json($"It's a auth ping {User.Identity.Name}!");
        }
    }
}
