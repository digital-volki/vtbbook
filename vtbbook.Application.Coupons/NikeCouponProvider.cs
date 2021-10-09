using System;
using vtbbook.Application.Domain.Models;

namespace vtbbook.Application.Coupons
{
    public class NikeCouponProvider : ICouponProvider
    {
        private static TimeSpan CouponExpire = TimeSpan.FromDays(10);

        public Coupon GenerateCoupon()
        {
            return new Coupon
            {
                Data = Guid.NewGuid().ToString(),
                Expire = DateTime.UtcNow + CouponExpire
            };
        }
    }
}
