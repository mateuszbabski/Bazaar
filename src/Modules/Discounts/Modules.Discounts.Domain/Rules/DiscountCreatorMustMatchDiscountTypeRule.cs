using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Exceptions;
using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.ValueObjects;

namespace Modules.Discounts.Domain.Rules
{
    internal class DiscountCreatorMustMatchDiscountTypeRule : IBusinessRule
    {
        private readonly Guid _creatorId;
        private readonly Roles _creatorRole;
        private readonly DiscountType _discountType;
        private readonly Guid? _targetId;

        public DiscountCreatorMustMatchDiscountTypeRule(Guid creatorId,
                                                        Roles creatorRole,
                                                        DiscountType discountType,
                                                        Guid? targetId)
        {
            _creatorId = creatorId;
            _creatorRole = creatorRole;
            _discountType = discountType;
            _targetId = targetId;
        }
        public string Message => "You dont have an access to proceed this action or you provided wrong data";

        public bool IsBroken()
        {
            if (_creatorRole.ToString() == "admin" 
                && (_discountType.ToString() == "AssignedToProduct"
                || _discountType.ToString() == "AssignedToVendors"
                || _discountType.ToString() == "AssignedToCustomer"))
            {
                return true;
            }

            if (_creatorRole.ToString() == "shop"
                && (_discountType.ToString() == "AssignedToAllProducts"
                || _discountType.ToString() == "AssignedToShipping"
                || _discountType.ToString() == "AssignedToOrderTotal"))
            {
                return true;
            }

            if (_creatorRole.ToString() == "shop" && _discountType.ToString() == "AssignedToVendors" && _creatorId != _targetId)
            {
                return true;
            }

            if (_creatorRole.ToString() == "shop"
                && _discountType.ToString() == "AssignedToProduct"
                && _targetId == Guid.Empty)
            {
                return true;
            }

            return false;
        }
    }
}
