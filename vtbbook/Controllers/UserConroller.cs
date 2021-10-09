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

            var user = new User
            {
                Email = userModel.Email,
                Password = userModel.Password
            };

            Guid id = _userService.UserRegistration(user);

            if (id == Guid.Empty)
            {
                return new StatusCodeResult(500);
            }

            string token = _userService.UserAuth(user);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            return Json($"{token}");
        }

        [Route("auth")]
        [HttpPost]
        public IActionResult Authorization([FromBody] UserModel userModel)
        {
            string token = _userService.UserAuth(new User
            {
                Email = userModel.Email,
                Password = userModel.Password
            });

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
            if (coupons is null)
            {
                return NotFound("Пользователь не найден");
            }
            return Json(coupons.ToList());
        }
    }
}
