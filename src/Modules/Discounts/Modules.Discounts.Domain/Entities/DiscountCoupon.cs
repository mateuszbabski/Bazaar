using Modules.Discounts.Domain.Exceptions;
using Modules.Discounts.Domain.Rules;
using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain;
using System.Text.Json.Serialization;

namespace Modules.Discounts.Domain.Entities
{
    public class DiscountCoupon : Entity
    {
        public DiscountCouponId Id { get; private set; }
        public DiscountId DiscountId { get; private set; }
        public Guid CreatedBy { get; private set; }
        public string DiscountCode { get; private set; }
        public DateTimeOffset StartsAt { get; private set; } = DateTimeOffset.Now;
        public DateTimeOffset ExpirationDate { get; private set; } = DateTimeOffset.Now.AddYears(1);
        public bool IsEnable { get; private set; } = true;
        [JsonIgnore]
        public virtual Discount Discount { get; private set; }

        private DiscountCoupon() { }
        private DiscountCoupon(Discount discount, DateTimeOffset startsAt, DateTimeOffset expirationDate) 
        {
            Id = new DiscountCouponId(Guid.NewGuid());
            CreatedBy = discount.CreatedBy;
            DiscountId = discount.Id;
            DiscountCode = SetDiscountCode(Guid.NewGuid());
            StartsAt = startsAt;
            ExpirationDate = expirationDate;
            IsEnable = true;
            Discount = discount;
        }

        internal static DiscountCoupon CreateDiscountCoupon(Discount discount, DateTimeOffset startsAt, DateTimeOffset expirationDate)
        {
            if (new DiscountCouponHasToStartBeforeExpirationDateRule(startsAt, expirationDate).IsBroken())
            {
                throw new InvalidDiscountCouponExpirationDateException();
            }

            var coupon = new DiscountCoupon(discount, startsAt, expirationDate);
            return coupon;
        }

        public void DisableCoupon()
        {
            this.IsEnable = false;
        }

        private string SetDiscountCode(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new InvalidDiscountCodeException();
            }

            var discountCode = id.ToString()[..8];

            return discountCode;
        }
    }
}
