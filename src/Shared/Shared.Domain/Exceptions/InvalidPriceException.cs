namespace Shared.Domain.Exceptions
{
    public class InvalidPriceException : Exception
    {
        public InvalidPriceException() : base(message: "Invalid price.")
        {

        }

        public InvalidPriceException(string message) : base(message)
        {

        }
    }
}
