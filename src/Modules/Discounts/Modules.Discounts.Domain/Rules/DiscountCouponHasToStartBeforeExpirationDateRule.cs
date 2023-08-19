using Shared.Domain;

namespace Modules.Discounts.Domain.Rules
{
    public class DiscountCouponHasToStartBeforeExpirationDateRule : IBusinessRule
    {
        private readonly DateTimeOffset _expires;
        private readonly DateTimeOffset _starts;

        public DiscountCouponHasToStartBeforeExpirationDateRule(DateTimeOffset expires, DateTimeOffset starts)
        {
            _expires = expires;
            _starts = starts;
        }

        public string Message => "Discount Coupon has expired";

        public bool IsBroken()
        {
            return _starts >= _expires;
        }
    }
}
