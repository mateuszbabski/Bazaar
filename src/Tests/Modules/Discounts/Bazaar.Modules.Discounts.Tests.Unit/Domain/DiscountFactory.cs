using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;

namespace Bazaar.Modules.Discounts.Tests.Unit.Domain
{
    public class DiscountFactory
    {
        public static Discount GetValueDiscount()
        {
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToAllProducts, Guid.Empty);

            var discount = Discount.CreateValueDiscount(Guid.NewGuid(), 10, "USD", discountTarget);

            return discount;
        }

        public static Discount GetPercentageDiscount()
        {
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToAllProducts, null);

            var discount = Discount.CreatePercentageDiscount(Guid.NewGuid(), 10, discountTarget);

            return discount;
        }
    }
}
