using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain;

namespace Modules.Discounts.Domain.Rules
{
    public class DiscountTargetHasToHaveTargetIdRule : IBusinessRule
    {
        private readonly DiscountType _discountType;
        private readonly Guid? _targetId;

        public DiscountTargetHasToHaveTargetIdRule(DiscountType discountType, Guid? targetId)
        {
            _discountType = discountType;
            _targetId = targetId;
        }
        public string Message => "Discount type misses targetId";

        public bool IsBroken()
        {
            return ((_discountType == DiscountType.AssignedToProduct
                || _discountType == DiscountType.AssignedToVendors
                || _discountType == DiscountType.AssignedToCustomer)
                && _targetId == Guid.Empty);
        }
    }
}
