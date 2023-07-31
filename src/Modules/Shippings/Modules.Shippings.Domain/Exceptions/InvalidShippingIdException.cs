namespace Modules.Shippings.Domain.Exceptions
{
    public class InvalidShippingIdException : Exception
    {
        public InvalidShippingIdException() : base(message: "Shipping id can't be empty.")
        {
            
        }
    }
}
