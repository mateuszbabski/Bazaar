using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Exceptions;
using Modules.Discounts.Domain.ValueObjects;

namespace Bazaar.Modules.Discounts.Tests.Unit.Domain
{
    public class DiscountDomainTest
    {
        [Fact]
        public void CreatePercentageDiscount_ReturnDiscountIfParamsAreValid()
        {
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToAllProducts, Guid.Empty);

            var discount = Discount.CreatePercentageDiscount(Guid.NewGuid(), 10, discountTarget);

            Assert.NotNull(discount);
            Assert.IsType<Discount>(discount);
            Assert.Null(discount.Currency);
        }

        [Fact]
        public void CreateValueDiscount_ReturnDiscountIfParamsAreValid()
        {
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToAllProducts, null);

            var discount = Discount.CreateValueDiscount(Guid.NewGuid(), 10, "USD", discountTarget);

            Assert.NotNull(discount);
            Assert.IsType<Discount>(discount);
            Assert.Equal(Guid.Empty, discount.DiscountTarget.TargetId);
        }

        [Fact]
        public void CreatePercentageDiscount_ReturnDiscountIfAllParamsAreValid()
        {
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToCustomer, Guid.NewGuid());

            var discount = Discount.CreatePercentageDiscount(Guid.NewGuid(), 10, discountTarget);

            Assert.NotNull(discount);
            Assert.IsType<Discount>(discount);
            Assert.Null(discount.Currency);
        }

        [Fact]
        public void CreateValueDiscount_ReturnDiscountIfAllParamsAreValid()
        {
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToCustomer, Guid.NewGuid());

            var discount = Discount.CreateValueDiscount(Guid.NewGuid(), 10, "USD", discountTarget);

            Assert.NotNull(discount);
            Assert.IsType<Discount>(discount);
        }

        [Fact]
        public void CreateValueDiscount_ReturnDiscountWithDefaultAmount()
        {
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToCustomer, Guid.NewGuid());

            var act = Assert.Throws<InvalidDiscountValueException>(() =>
            Discount.CreateValueDiscount(Guid.NewGuid(), 0, "USD", discountTarget));

            Assert.IsType<InvalidDiscountValueException>(act);
        }

        [Fact]
        public void CreateDiscountTarget_ThrowsIsThereIsMissingTargetId()
        {
            var act = Assert.Throws<MissingRequiredTargetIdException>(()
                => DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToCustomer, Guid.Empty));

            Assert.IsType<MissingRequiredTargetIdException>(act);
        }

        [Fact]
        public void CreateDiscountTarget_ReturnsDiscountTargetWithValidParams()
        {
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToCustomer, Guid.NewGuid());

            Assert.IsType<DiscountTarget>(discountTarget);
            Assert.Equal(DiscountType.AssignedToCustomer, discountTarget.DiscountType);
        }

        [Fact]
        public void CreateDiscountTarget_ReturnsDiscountTargetWithEmptyGuid()
        {
            var guid = Guid.NewGuid();
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToOrderTotal, guid);

            Assert.IsType<DiscountTarget>(discountTarget);
            Assert.Equal(DiscountType.AssignedToOrderTotal, discountTarget.DiscountType);
            Assert.Equal(Guid.Empty, discountTarget.TargetId);
        }

        //[Fact]
        //public void CreatePercentageDiscount_ThrowsIfThereIsMissingTargetId()
        //{
        //    var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToCustomer, Guid.Empty);

        //    var act = Assert.Throws<MissingRequiredTargetIdException>(()
        //        => Discount.CreatePercentageDiscount(10, discountTarget));

        //    Assert.IsType<MissingRequiredTargetIdException>(act);
        //}

        //[Fact]
        //public void CreateValueDiscount_ThrowsIfThereIsMissingTargetId()
        //{
        //    var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToCustomer, null);

        //    var act = Assert.Throws<MissingRequiredTargetIdException>(()
        //        => Discount.CreateValueDiscount(10, discountTarget, "USD"));

        //    Assert.IsType<MissingRequiredTargetIdException>(act);
        //}

        [Fact]
        public void CreateValueDiscount_ThrowsIfCurrencyNotApprovedBySystem()
        {
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToCustomer, Guid.NewGuid());

            var act = Assert.Throws<InvalidDiscountCurrencyException>(()
                => Discount.CreateValueDiscount(Guid.NewGuid(), 10, "PES", discountTarget));

            Assert.IsType<InvalidDiscountCurrencyException>(act);
        }

        [Fact]
        public void CreateDiscountCoupon_ReturnsDiscountCouponWithValidParams()
        {
            var discount = DiscountFactory.GetValueDiscount();
            var discountCoupon = discount.CreateNewDiscountCoupon(discount.CreatedBy,
                                                                  DateTimeOffset.UtcNow,
                                                                  DateTimeOffset.UtcNow.AddDays(7));

            Assert.IsType<DiscountCoupon>(discountCoupon);
        }

        [Fact]
        public void CreateDiscountCoupon_ThrowsWithInvalidDateTimeParams()
        {
            var discount = DiscountFactory.GetValueDiscount();
            var act = Assert.Throws<InvalidDiscountCouponExpirationDateException>(() 
                => discount.CreateNewDiscountCoupon(discount.CreatedBy,
                                                    DateTimeOffset.UtcNow.AddDays(7),
                                                    DateTimeOffset.UtcNow.AddDays(1)));

            Assert.IsType<InvalidDiscountCouponExpirationDateException>(act);
        }

        [Fact]
        public void DisableDiscountCoupon_DisablesCouponProperly()
        {
            var discount = DiscountFactory.GetValueDiscount();
            var discountCoupon = discount.CreateNewDiscountCoupon(discount.CreatedBy,
                                                                  DateTimeOffset.UtcNow,
                                                                  DateTimeOffset.UtcNow.AddDays(7));

            Assert.IsType<DiscountCoupon>(discountCoupon);
            Assert.True(discountCoupon.IsEnable);

            discountCoupon.DisableCoupon();
            Assert.False(discountCoupon.IsEnable);
        }
    }
}
