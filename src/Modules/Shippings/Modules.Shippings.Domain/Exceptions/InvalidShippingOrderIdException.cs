namespace Modules.Shippings.Domain.Exceptions
{
    public class InvalidShippingOrderIdException : Exception
    {
        public InvalidShippingOrderIdException() : base(message: "Shipping order id can't be empty.")
        {

        }
    }
}
