using Shared.Domain.Exceptions;

namespace Modules.Products.Domain.Exceptions
{
    public class InvalidProductPriceException : InvalidPriceException
    {
        public InvalidProductPriceException(string message) : base(message)
        {
        }
    }
}
