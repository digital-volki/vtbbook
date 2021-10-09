using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using vtbbook.Application.Domain.Models;
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
            if (_userService.UserExist(userModel.Email))
            {
                return Conflict();
            }
            User user = new();
            user.Email = userModel.Email;
            user.Password = userModel.Password;
            Guid id = _userService.UserRegistration(user);
            return Json($"{id}");
        }

        [Route("auth")]
        [HttpPost]
        public IActionResult Authorization([FromBody] UserModel userModel)
        {
            User user = new();
            user.Email = userModel.Email;
            user.Password = userModel.Password;
            string token = _userService.UserAuth(user);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            return Json($"{token}");
        }

        [Authorize]
        [Route("profile/balance")]
        [HttpGet]
        public IActionResult GetBalance()
        {
            var balance = _userService.GetCurrentBalance(GetUserId(User));
            if (balance is null)
            {
                return NotFound("Пользователь не найден");
            }
            return Json($"{balance}");
        }

        [Authorize]
        [Route("profile/coupons")]
        [HttpGet]
        public IActionResult GetCoupons([FromQuery] int page, [FromQuery] int count)
        {
            var coupons = _userService.GetCoupons(GetUserId(User), page, count);
            return Json(coupons.ToList());
        }
    }
}
