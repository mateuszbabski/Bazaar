using Modules.Discounts.Domain.Events;
using Modules.Discounts.Domain.Exceptions;
using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.Rules;

namespace Modules.Discounts.Domain.Entities
{ // TODO: think about putting discount target logic into discount entity to omit pack logic in handlers
    public class Discount : Entity, IAggregateRoot
    {
        #nullable enable
        public DiscountId Id { get; private set; }
        public decimal DiscountValue { get; private set; }
        public bool IsPercentageDiscount { get; private set; }
        public string? Currency { get; private set; } = string.Empty;
        public DiscountTarget DiscountTarget { get; private set; }
        public virtual List<DiscountCoupon> DiscountCoupons { get; private set; }

        private Discount() { }
        private Discount(decimal discountValue,
                         DiscountTarget discountTarget,
                         string currency)
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
                                                   string currency,
                                                   DiscountTarget discountTarget)
        {
            if (discountValue <= 0)
            {
                throw new InvalidDiscountValueException();
            }

            if (new SystemMustAcceptsCurrencyRule(currency).IsBroken())
            {
                throw new InvalidDiscountCurrencyException();
            }
            //var discountTarget = DiscountTarget.CreateDiscountTarget(discountType, targetId);
            var discount = new Discount(discountValue, discountTarget, currency);
            discount.AddDomainEvent(new NewDiscountCreatedDomainEvent(discount.Id));

            return discount;
        }

        public static Discount CreatePercentageDiscount(decimal discountValue,
                                                        DiscountTarget discountTarget)
        {
            if (discountValue <= 0)
            {
                throw new InvalidDiscountValueException();
            }
            //var discountTarget = DiscountTarget.CreateDiscountTarget(discountType, targetId);
            var discount = new Discount(discountValue, discountTarget);
            discount.AddDomainEvent(new NewDiscountCreatedDomainEvent(discount.Id));

            return discount;
        }

        internal void AddCouponToDiscount(DiscountCoupon discountCoupon)
        {
            this.DiscountCoupons.Add(discountCoupon);
            this.AddDomainEvent(new NewDiscountCouponAddedToList(discountCoupon.Id));
        }

        internal void DisableAllCoupons()
        {
            foreach(var coupon in this.DiscountCoupons)
            {
                coupon.DisableCoupon();
            }
        }
    }
}
