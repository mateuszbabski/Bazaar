using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Discounts.Domain.Exceptions
{
    public class InvalidDiscountCouponExpirationDateException : Exception
    {
        public InvalidDiscountCouponExpirationDateException() : base(message: "Coupon can't start after expiration date")
        {
            
        }
    }
}
