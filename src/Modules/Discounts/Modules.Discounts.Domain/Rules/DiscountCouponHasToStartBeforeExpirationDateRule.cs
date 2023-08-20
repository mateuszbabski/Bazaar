using Shared.Domain;

namespace Modules.Discounts.Domain.Rules
{
    public class DiscountCouponHasToStartBeforeExpirationDateRule : IBusinessRule
    {
        private readonly DateTimeOffset _starts;
        private readonly DateTimeOffset _expires;

        public DiscountCouponHasToStartBeforeExpirationDateRule(DateTimeOffset starts, DateTimeOffset expires)
        {
            _starts = starts;
            _expires = expires;
        }

        public string Message => "Discount Coupon has to start before expiration date";

        public bool IsBroken()
        {
            return _starts >= _expires;
        }
    }
}
