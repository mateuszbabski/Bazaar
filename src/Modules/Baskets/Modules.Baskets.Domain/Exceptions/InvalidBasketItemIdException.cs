using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Baskets.Domain.Exceptions
{
    public class InvalidBasketItemIdException : Exception
    {
        public InvalidBasketItemIdException() : base(message: "Basket Item Id can not be empty.")
        {

        }
    }
}
