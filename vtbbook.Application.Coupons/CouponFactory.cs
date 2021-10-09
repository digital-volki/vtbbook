namespace vtbbook.Application.Coupons
{
    public class CouponFactory : ICouponFactory
    {
        public ICouponProvider GetCouponProvider(string providerName)
        {
            return providerName switch
            {
                "Nike" => new NikeCouponProvider(),
                _ => new DefaultCouponProvider(),
            };
        }
    }
}
