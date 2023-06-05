namespace Shared.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base(message: "Invalid password")
        {
        }
    }
}
