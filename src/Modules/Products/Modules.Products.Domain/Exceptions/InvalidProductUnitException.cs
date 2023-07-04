namespace Modules.Products.Domain.Exceptions
{
    public class InvalidProductUnitException : Exception
    {
        public InvalidProductUnitException() : base(message: "Invalid product unit.")
        {
        }
    }
}
