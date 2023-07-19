using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Baskets.Domain.Exceptions
{
    public class InvalidBasketPriceException : Exception
    {
        public InvalidBasketPriceException(string message) : base(message)
        {

        }
    }
}
