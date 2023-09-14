using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;

namespace Bazaar.Modules.Discounts.Tests.Unit.Application
{
    public class DiscountListMock
    {
        private readonly List<Discount> _discountList;
        public List<Discount> Discounts
        {
            get { return _discountList; }
        }

        public DiscountListMock(Guid shopId)
        {
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToVendors, shopId);

            var discount1 = Discount.CreateValueDiscount(shopId, 10, "USD", discountTarget);
            var discount2 = Discount.CreatePercentageDiscount(shopId, 10, discountTarget);

            _discountList = new List<Discount>
            {
                discount1,
                discount2,
            };
        }
    }
}
