namespace Modules.Baskets.Domain.Exceptions
{
    public class InvalidBasketProductIdException : Exception
    {
        public InvalidBasketProductIdException() : base(message: "Basket Product Id is empty or invalid.")
        {
            
        }
    }
}
