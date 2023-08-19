using Shared.Domain;

namespace Modules.Discounts.Domain.Rules
{
    public class DiscountCouponCantBeExpiredRule : IBusinessRule
    {
        private readonly DateTimeOffset _expires;
        private readonly DateTimeOffset _utcNow;

        public DiscountCouponCantBeExpiredRule(DateTimeOffset expires, DateTimeOffset utcNow)
        {
            _expires = expires;
            _utcNow = utcNow;
        }

        public string Message => "Discount Coupon has expired";

        public bool IsBroken()
        {
            return _expires <= _utcNow;
        }
        
    }
}
