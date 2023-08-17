using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Discounts.Domain.Exceptions
{
    public class InvalidDiscountCodeException : Exception
    {
        public InvalidDiscountCodeException() : base(message: "Discount Code cannot be empty.")
        {

        }
    }
}
