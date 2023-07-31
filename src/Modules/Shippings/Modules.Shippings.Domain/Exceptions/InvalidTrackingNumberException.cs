namespace Modules.Shippings.Domain.Exceptions
{
    public class InvalidTrackingNumberException : Exception
    {
        public InvalidTrackingNumberException() : base(message: "Invalid tracking number.")
        {
            
        }
    }
}
