using Microsoft.AspNetCore.Mvc;
using System;
using vtbbook.Application.Service;
using vtbbook.Models;

namespace vtbbook.Controllers
{
    [ApiController]
    public class UserConroller : VtbConrollerBase
    {
        private readonly IUserService _userService;
        public UserConroller(IUserService userService)
        {
            _userService = userService;
        }

        [Route("auth/reg")]
        [HttpPost]
        public IActionResult Registration([FromBody] UserModel userModel)
        {
            User user = new();
            user.Email = userModel.Email;
            user.Password = userModel.Password;
            Guid id = _userService.UserRegistration(user);
            return Json($"{id}");
        }

        [Route("auth")]
        [HttpGet]
        public IActionResult Authorization()
        {
            return Json($"It's a auth ping {User.Identity.Name}!");
        }
    }
}
