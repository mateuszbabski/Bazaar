using Modules.Discounts.Domain.Events;
using Modules.Discounts.Domain.Exceptions;
using Modules.Discounts.Domain.Rules;
using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.Rules;

namespace Modules.Discounts.Domain.Entities
{ 
    public class Discount : Entity, IAggregateRoot
    {
        public DiscountId Id { get; private set; }
        public Guid CreatedBy { get; private set; }
        public decimal DiscountValue { get; private set; }
        public bool IsPercentageDiscount { get; private set; }
        public string? Currency { get; private set; } = string.Empty;
        public DiscountTarget DiscountTarget { get; private set; }
        public virtual List<DiscountCoupon> DiscountCoupons { get; private set; }

        private Discount() { }

        private Discount(Guid creator, 
                         decimal discountValue,
                         DiscountTarget discountTarget,
                         string currency)
        {
            Id = new DiscountId(Guid.NewGuid());
            CreatedBy = creator;
            DiscountValue = discountValue;
            IsPercentageDiscount = false;
            DiscountTarget = discountTarget;
            Currency = currency;
            DiscountCoupons = new List<DiscountCoupon>();
        }

        private Discount(Guid creator, 
                         decimal discountValue,
                         DiscountTarget discountTarget) 
        {            
            Id = new DiscountId(Guid.NewGuid());
            CreatedBy = creator;
            DiscountValue = discountValue;
            IsPercentageDiscount = true;
            DiscountTarget = discountTarget;
            Currency = null;
            DiscountCoupons = new List<DiscountCoupon>();
        }

        public static Discount CreateValueDiscount(Guid creator,
                                                   decimal discountValue,
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
            var discount = new Discount(creator, discountValue, discountTarget, currency);
            discount.AddDomainEvent(new NewDiscountCreatedDomainEvent(discount));

            return discount;
        }

        public static Discount CreatePercentageDiscount(Guid creator,
                                                        decimal discountValue,
                                                        DiscountTarget discountTarget)
        {
            if (discountValue <= 0 || discountValue >= 100)
            {
                throw new InvalidDiscountValueException();
            }
            //var discountTarget = DiscountTarget.CreateDiscountTarget(discountType, targetId);
            var discount = new Discount(creator, discountValue, discountTarget);
            discount.AddDomainEvent(new NewDiscountCreatedDomainEvent(discount));

            return discount;
        }

        public DiscountCoupon CreateNewDiscountCoupon(Guid userId,
                                                      DateTimeOffset startsAt,
                                                      DateTimeOffset expirationDate)
        {
            if (new DiscountCouponCanBeAddOnlyByDiscountCreatorRule(userId, this.CreatedBy).IsBroken())
            {
                throw new ActionForbiddenException();
            }

            var discountCoupon = DiscountCoupon.CreateDiscountCoupon(this, startsAt, expirationDate);
            AddCouponToDiscount(discountCoupon);
            return discountCoupon;
        }

        internal void AddCouponToDiscount(DiscountCoupon discountCoupon)
        {
            this.DiscountCoupons.Add(discountCoupon);
            this.AddDomainEvent(new NewDiscountCouponAddedToListDomainEvent(discountCoupon.DiscountCode,
                                                                            this.DiscountTarget));
        }

        internal void DisableAllCoupons()
        {
            this.DiscountCoupons.ForEach(x => x.DisableCoupon());
        }
    }
}
