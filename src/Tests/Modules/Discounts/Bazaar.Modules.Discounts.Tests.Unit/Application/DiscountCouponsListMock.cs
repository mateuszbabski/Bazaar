using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;

namespace Bazaar.Modules.Discounts.Tests.Unit.Application
{
    public class DiscountCouponsListMock
    {
        public static List<DiscountCoupon> GetDiscountCoupons(Guid shopId, Guid customerId)
        {
            var shop2Id = Guid.NewGuid();

            var discountTarget1 = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToCustomer, customerId);
            var discount1 = Discount.CreateValueDiscount(shopId, 10, "USD", discountTarget1);
            var discountCoupon11 = discount1.CreateNewDiscountCoupon(shopId, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(7));
            var discountCoupon12 = discount1.CreateNewDiscountCoupon(shopId, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(7));

            var discountTarget2 = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToVendors, shopId);
            var discount2 = Discount.CreateValueDiscount(shopId, 10, "USD", discountTarget2);
            var discountCoupon21 = discount2.CreateNewDiscountCoupon(shopId, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(7));
            var discountCoupon22 = discount2.CreateNewDiscountCoupon(shopId, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(7));

            var discountTarget3 = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToVendors, shop2Id);
            var discount3 = Discount.CreateValueDiscount(shop2Id, 10, "USD", discountTarget3);
            var discountCoupon31 = discount3.CreateNewDiscountCoupon(shop2Id, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(7));
            var discountCoupon32 = discount3.CreateNewDiscountCoupon(shop2Id, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(7));

            var couponsList = new List<DiscountCoupon>()
            {
                discountCoupon11,
                discountCoupon12,
                discountCoupon21,
                discountCoupon22,
                discountCoupon31,
                discountCoupon32
            };

            return couponsList;
        }

        public static List<Discount> GetDiscounts(Guid shopId, Guid customerId)
        {
            var shop2Id = Guid.NewGuid();

            var discountTarget1 = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToCustomer, customerId);
            var discount1 = Discount.CreateValueDiscount(shopId, 10, "USD", discountTarget1);
            var discountCoupon11 = discount1.CreateNewDiscountCoupon(shopId, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(1));
            var discountCoupon12 = discount1.CreateNewDiscountCoupon(shopId, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(7));

            var discountTarget2 = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToVendors, shopId);
            var discount2 = Discount.CreateValueDiscount(shopId, 10, "USD", discountTarget2);
            var discountCoupon21 = discount2.CreateNewDiscountCoupon(shopId, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(7));
            var discountCoupon22 = discount2.CreateNewDiscountCoupon(shopId, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(7));

            var discountTarget3 = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToVendors, shop2Id);
            var discount3 = Discount.CreateValueDiscount(shop2Id, 10, "USD", discountTarget3);
            var discountCoupon31 = discount3.CreateNewDiscountCoupon(shop2Id, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(7));
            var discountCoupon32 = discount3.CreateNewDiscountCoupon(shop2Id, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(7));

            var discountsList = new List<Discount>()
            {
                discount1,
                discount2,
                discount3
            };

            return discountsList;
        }
    }
}
