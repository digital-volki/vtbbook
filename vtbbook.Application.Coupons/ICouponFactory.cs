namespace vtbbook.Application.Coupons
{
    public interface ICouponFactory
    {
        ICouponProvider GetCouponProvider(string providerName);
    }
}