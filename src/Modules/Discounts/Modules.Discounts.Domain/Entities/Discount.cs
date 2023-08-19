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
        public decimal DiscountValue { get; private set; }
        public bool IsPercentageDiscount { get; private set; }
        public Currency? Currency { get; private set; }
        public DiscountTarget DiscountTarget { get; private set; }
        public virtual List<DiscountCoupon> DiscountCoupons { get; private set; }

        private Discount() { }

        private Discount(decimal discountValue,
                         DiscountTarget discountTarget,
                         Currency currency)
        {
            Id = new DiscountId(Guid.NewGuid());
            DiscountValue = discountValue;
            IsPercentageDiscount = false;
            DiscountTarget = discountTarget;
            Currency = currency;
            DiscountCoupons = new List<DiscountCoupon>();
        }

        private Discount(decimal discountValue,
                         DiscountTarget discountTarget) 
        {
            Id = new DiscountId(Guid.NewGuid());
            DiscountValue = discountValue;
            IsPercentageDiscount = true;
            DiscountTarget = discountTarget;
            Currency = null;
            DiscountCoupons = new List<DiscountCoupon>();
        }

        public static Discount CreateValueDiscount(decimal discountValue,
                                                   DiscountTarget discountTarget,
                                                   Currency currency)
        {
            if (new SystemMustAcceptsCurrencyRule(currency.ToString()).IsBroken())
            {
                throw new InvalidDiscountCurrencyException();
            }

            var discount = new Discount(discountValue, discountTarget, currency);
            discount.AddDomainEvent(new NewDiscountCreatedDomainEvent(discount.Id));

            return discount;
        }

        public static Discount CreatePercentageDiscount(decimal discountValue,
                                                        DiscountTarget discountTarget)
        {
            var discount = new Discount(discountValue, discountTarget);
            discount.AddDomainEvent(new NewDiscountCreatedDomainEvent(discount.Id));

            return discount;
        }

        public void AddCouponToDiscount(DiscountCoupon discountCoupon)
        {
            this.DiscountCoupons.Add(discountCoupon);
            this.AddDomainEvent(new NewDiscountCouponAddedToList(discountCoupon.Id));
        }
    }
}
