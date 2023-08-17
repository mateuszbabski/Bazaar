using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Discounts.Domain.ValueObjects
{
    public record DiscountCouponId
    {
        public Guid Value { get; }

        public DiscountCouponId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidDiscountCouponIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(DiscountCouponId id) => id.Value;
        public static implicit operator DiscountCouponId(Guid value) => new(value);
    }
}
