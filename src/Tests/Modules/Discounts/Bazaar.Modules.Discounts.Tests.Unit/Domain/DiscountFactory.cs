using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;

namespace Bazaar.Modules.Discounts.Tests.Unit.Domain
{
    public class DiscountFactory
    {
        public static Discount GetValueDiscount()
        {
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToAllProducts, Guid.Empty);

            var discount = Discount.CreateValueDiscount(10, discountTarget, "USD");

            return discount;
        }

        public static Discount GetPercentageDiscount()
        {
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToAllProducts, null);

            var discount = Discount.CreatePercentageDiscount(10, discountTarget);

            return discount;
        }
    }
}
