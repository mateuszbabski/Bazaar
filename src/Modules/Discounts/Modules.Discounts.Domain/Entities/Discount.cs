using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.ValueObjects;

namespace Modules.Discounts.Domain.Entities
{
    public class Discount : Entity, IAggregateRoot
    {
        public DiscountId Id { get; private set; }
        public DiscountCouponId DiscountCouponId { get; private set; }
        public DiscountType DiscountType { get; private set; }
        public decimal DiscountValue { get; private set; }
        public bool IsPercentageDiscount { get; private set; }
        public Currency Currency { get; private set; }
        public virtual DiscountCoupon DiscountCoupon { get; private set; }

        private Discount() { }

        //public static Discount CreateDiscount() 
        //{
        //}
    }
}
