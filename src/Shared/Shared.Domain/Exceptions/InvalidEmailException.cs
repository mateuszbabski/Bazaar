namespace Shared.Domain.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base(message: "Invalid email.")
        {
        }
    }
}
