using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain;

namespace Modules.Discounts.Domain.Entities
{
    public class DiscountCoupon : Entity
    {
        public DiscountCouponId Id { get; private set; }
        public DiscountCode DiscountCode { get; private set; }
        public DateTimeOffset StartsAt { get; private set; } = DateTimeOffset.Now;
        public DateTimeOffset ExpirationDate { get; private set; }
        public bool IsEnable { get; private set; } = true;

        private DiscountCoupon(DateTimeOffset startsAt, DateTimeOffset expirationDate) 
        {
            Id = new DiscountCouponId(Guid.NewGuid());
            DiscountCode = new DiscountCode(Guid.NewGuid());
            StartsAt = startsAt;
            ExpirationDate = expirationDate;
            IsEnable = true;
        } 

        public static DiscountCoupon CreateDiscountCoupon(DateTimeOffset startsAt, DateTimeOffset expirationDate)
        {
            var coupon = new DiscountCoupon(startsAt, expirationDate);
            return coupon;
        }

        internal void DisableCoupon()
        {
            this.IsEnable = false;
        }
    }
}
