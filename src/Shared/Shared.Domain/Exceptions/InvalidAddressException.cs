namespace Shared.Domain.Exceptions
{
    public class InvalidAddressException : Exception
    {
        public InvalidAddressException() : base(message: "Invalid address.")
        {

        }
    }
}
