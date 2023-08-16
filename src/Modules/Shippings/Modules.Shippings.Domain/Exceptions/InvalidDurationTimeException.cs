namespace Modules.Shippings.Domain.Exceptions
{
    public class InvalidDurationTimeException : Exception
    {
        public InvalidDurationTimeException() : base(message: "Delivery time can't be 0 or lower")
        {
            
        }
    }
}
