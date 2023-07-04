namespace Modules.Products.Domain.Exceptions
{
    public class InvalidProductCategoryException : Exception
    {
        public InvalidProductCategoryException() : base(message: "Invalid product category.")
        {
            
        }
    }
}
