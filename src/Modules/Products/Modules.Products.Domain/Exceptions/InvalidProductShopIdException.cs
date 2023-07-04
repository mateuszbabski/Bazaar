namespace Modules.Products.Domain.Exceptions
{
    public class InvalidProductShopIdException : Exception
    {
        public InvalidProductShopIdException() : base(message: "Invalid Shop Id.")
        {
        }
    }
}
