namespace Modules.Products.Domain.Exceptions
{
    public class InvalidProductNameException : Exception
    {
        public InvalidProductNameException() : base(message: "Product name cannot be empty.")
        {

        }
    }
}
