using System;
using System.Collections.Generic;
using System.Linq;
using vtbbook.Application.Coupons;
using vtbbook.Application.Domain;
using vtbbook.Application.Domain.Models;
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Application.Service
{
    public class StoreService : IStoreService
    {
        private readonly IStoreDomain _storeDomain;
        private readonly ICouponDomain _couponDomain;
        private readonly ICouponFactory _couponFactory;
        private readonly IUserService _userService;

        public StoreService(
            IStoreDomain storeDomain, 
            ICouponDomain couponDomain, 
            ICouponFactory couponFactory, 
            IUserService userService)
        {
            _storeDomain = storeDomain;
            _couponDomain = couponDomain;
            _couponFactory = couponFactory;
            _userService = userService;
        }

        public IEnumerable<Product> GetProducts(int page, int count)
        {
            return _storeDomain.Get()
                .Skip((page - 1) * count)
                .Take(count)
                .Select(x => new Product
                {
                    Title = x.Title,
                    Description = x.Description,
                    Price = x.Price,
                    Discount = x.Discount
                });
        }

        public Coupon BuyProduct(Guid userId, Guid productId)
        {
            var dbProduct = GetProduct(productId);
            if (dbProduct is null || !_userService.UserExist(userId))
            {
                return null;
            }

            var coupon = _couponFactory.GetCouponProvider(dbProduct.Title).GenerateCoupon();
            _userService.AddCurrency(userId, -dbProduct.Price);

            var dbCoupon = _couponDomain.Add(new DbCoupon
            {
                Data = coupon.Data,
                Expire = coupon.Expire,
                Product = dbProduct
            });

            if (dbCoupon is null)
            {
                return null;
            }

            return coupon;
        }

        public DbProduct GetProduct(Guid productId)
        {
            return _storeDomain.Get().FirstOrDefault(x => x.Id == productId);
        }
    }
}
