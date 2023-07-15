namespace Modules.Baskets.Domain.Exceptions
{
    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException() : base(message: "Invalid quantity.")
        {
        }
    }
}
