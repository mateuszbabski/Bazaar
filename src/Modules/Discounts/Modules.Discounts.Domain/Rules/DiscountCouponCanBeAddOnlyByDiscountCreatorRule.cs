using Shared.Domain;

namespace Modules.Discounts.Domain.Rules
{
    internal class DiscountCouponCanBeAddOnlyByDiscountCreatorRule : IBusinessRule
    {
        private readonly Guid _userId;
        private readonly Guid _discountCreator;

        public DiscountCouponCanBeAddOnlyByDiscountCreatorRule(Guid userId, Guid discountCreator)
        {
            _userId = userId;
            _discountCreator = discountCreator;
        }

        public string Message => "Only discount creator can add discount coupons";

        public bool IsBroken()
        {
            return _userId != _discountCreator;
        }
    }
}
