namespace Shared.Domain.Exceptions
{
    public class InvalidTelephoneNumberException : Exception
    {
        public InvalidTelephoneNumberException() : base(message: "Invalid number.")
        {
        }
    }
}
