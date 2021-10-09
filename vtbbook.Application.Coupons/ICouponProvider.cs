using vtbbook.Application.Domain.Models;

namespace vtbbook.Application.Coupons
{
    public interface ICouponProvider
    {
        Coupon GenerateCoupon();
    }
}