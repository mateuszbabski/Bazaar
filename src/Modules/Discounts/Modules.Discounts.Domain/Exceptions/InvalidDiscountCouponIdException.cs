using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Discounts.Domain.Exceptions
{
    public class InvalidDiscountCouponIdException : Exception
    {
        public InvalidDiscountCouponIdException() : base(message: "Discount Coupon Id cannot be empty.")
        {

        }
    }
}
