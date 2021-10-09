using System;
using vtbbook.Application.Domain.Models;

namespace vtbbook.Application.Coupons
{
    public class DefaultCouponProvider : ICouponProvider
    {
        private static TimeSpan CouponExpire = TimeSpan.FromDays(30);

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
