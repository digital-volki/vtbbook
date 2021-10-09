using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using vtbbook.Application.Service;

namespace vtbbook.Controllers
{
    [ApiController]
    public class StoreConroller : VtbConrollerBase
    {
        private readonly IStoreService _storeService;
        private readonly IUserService _userService;

        public StoreConroller(IStoreService storeService, IUserService userService)
        {
            _storeService = storeService;
            _userService = userService;
        }

        [Route("store")]
        [HttpGet]
        public IActionResult GetProducts([FromQuery] int page, [FromQuery] int count)
        {
            var products = _storeService.GetProducts(page, count);
            return Json(products.ToList());
        }

        [Authorize]
        [Route("store/buy/{productId}")]
        [HttpPost]
        public IActionResult BuyProduct([FromRoute] Guid productId)
        {
            var product = _storeService.GetProduct(productId);
            if (product is null)
            {
                return NotFound("Продукт не существует.");
            }

            if (!_userService.IsEnoughFunds(GetUserId(User), product.Price))
            {
                return Forbid();
            }

            var coupon = _storeService.BuyProduct(GetUserId(User), productId);
            if (coupon == null)
            {
                return NotFound("Не удалось зарезервировать купон");
            }

            return Json(coupon);
        }
    }
}
