namespace Modules.Products.Domain.Exceptions
{
    public class InvalidProductIdException : Exception
    {
        public InvalidProductIdException() : base(message: "Product Id cannot be empty.")
        {

        }
    }
}
