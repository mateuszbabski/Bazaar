using Modules.Discounts.Domain.Exceptions;
using Modules.Discounts.Domain.Rules;
using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain.ValueObjects;

namespace Modules.Discounts.Domain.Entities
{
    public class DiscountTarget
    {
        public DiscountType DiscountType { get; set; }
        public Guid? TargetId { get; set; } = Guid.Empty;

        private DiscountTarget(DiscountType discountType, Guid? targetId)
        {
            DiscountType = discountType;
            TargetId = targetId;
        }

        public static DiscountTarget CreateDiscountTarget(DiscountType discountType,
                                                          Guid? targetId)
        {
            //if (new DiscountCreatorMustMatchDiscountTypeRule(creatorId, creatorRole, discountType, targetId).IsBroken())
            //{
            //    throw new ActionForbiddenException();
            //}

            if (new DiscountTargetHasToHaveTargetIdRule(discountType, targetId).IsBroken())
            {
                throw new MissingRequiredTargetIdException();
            }            

            if (CanOmitTargetId(discountType))
            {
                targetId = Guid.Empty;
            }

            return new DiscountTarget(discountType, targetId);
        }

        private static bool CanOmitTargetId(DiscountType discountType)
        {
            return (discountType == DiscountType.AssignedToOrderTotal
                || discountType == DiscountType.AssignedToShipping
                || discountType == DiscountType.AssignedToAllProducts);
        }

    }
}
