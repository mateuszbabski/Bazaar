using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain;

namespace Modules.Discounts.Domain.Entities
{
    public class DiscountCoupon : Entity
    {
        public DiscountCouponId Id { get; private set; }
        public DiscountCode DiscountCode { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public DateTimeOffset ExpirationDate { get; private set; }
        public bool IsEnable { get; private set; } = true;

        private DiscountCoupon(DateTimeOffset createdOn, DateTimeOffset expirationDate) 
        {
            Id = new DiscountCouponId(Guid.NewGuid());
            DiscountCode = new DiscountCode(Guid.NewGuid());
            CreatedAt = createdOn;
            ExpirationDate = expirationDate;
            IsEnable = true;
        } 

        public static DiscountCoupon CreateDiscountCoupon(DateTimeOffset createdOn, DateTimeOffset expirationDate)
        {
            var coupon = new DiscountCoupon(createdOn, expirationDate);
            return coupon;
        }

        internal void DisableCoupon()
        {
            this.IsEnable = false;
        }
    }
}
