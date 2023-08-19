using Modules.Discounts.Domain.Events;
using Modules.Discounts.Domain.Exceptions;
using Modules.Discounts.Domain.Rules;
using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.Exceptions;
using Shared.Domain.Rules;
using Shared.Domain.ValueObjects;

namespace Modules.Discounts.Domain.Entities
{
    public class Discount : Entity, IAggregateRoot
    {
        public DiscountId Id { get; private set; }
        public DiscountCouponId DiscountCouponId { get; private set; }
        public decimal DiscountValue { get; private set; }
        public bool IsPercentageDiscount { get; private set; }
        public Currency? Currency { get; private set; }
        public DiscountTarget DiscountTarget { get; private set; }
        public virtual DiscountCoupon DiscountCoupon { get; private set; }

        private Discount() { }

        private Discount(DiscountCoupon discountCoupon,
                         decimal discountValue,
                         bool isPercentageDiscount,
                         DiscountTarget discountTarget,
                         Currency currency) 
        {
            Id = new DiscountId(Guid.NewGuid());
            DiscountCouponId = discountCoupon.Id;
            DiscountValue = discountValue;
            IsPercentageDiscount = isPercentageDiscount;
            DiscountTarget = discountTarget;
            Currency = currency;
            DiscountCoupon = discountCoupon;
        }

        public static Discount CreateDiscount(DiscountCoupon discountCoupon,
                                              decimal discountValue,
                                              bool isPercentageDiscount,
                                              DiscountTarget discountTarget,
                                              DateTimeOffset dateTimeUtcNow,
                                              Currency currency)
        {
            if (new DiscountCouponCantBeExpiredRule(discountCoupon.ExpirationDate, dateTimeUtcNow).IsBroken())
            {
                throw new InvalidDiscountCouponException();
            }

            var discount = new Discount(discountCoupon, discountValue, isPercentageDiscount, discountTarget, currency);
            discount.AddDomainEvent(new NewDiscountCreatedDomainEvent(discount.Id));

            return discount;
        }
    }
}
