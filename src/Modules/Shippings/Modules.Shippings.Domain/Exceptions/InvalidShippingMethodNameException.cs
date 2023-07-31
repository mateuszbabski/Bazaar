namespace Modules.Shippings.Domain.Exceptions
{
    public class InvalidShippingMethodNameException : Exception
    {
        public InvalidShippingMethodNameException() : base (message: "Shipping name can't be empty.")
        {
            
        }
    }
}
