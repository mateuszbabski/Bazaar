namespace Modules.Products.Domain.Exceptions
{
    public class InvalidProductDescriptionException : Exception
    {
        public InvalidProductDescriptionException() : base(message: "Product description cannot be empty.")
        {
            
        }
    }
}
