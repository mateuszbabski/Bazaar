using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Shippings.Domain.Exceptions
{
    public class InvalidShippingMethodIdException : Exception
    {
        public InvalidShippingMethodIdException() : base(message: "Shipping method id can't be empty.")
        {
            
        }
    }
}
