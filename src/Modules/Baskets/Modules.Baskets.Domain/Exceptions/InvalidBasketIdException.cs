namespace Modules.Baskets.Domain.Exceptions
{
    public class InvalidBasketIdException : Exception
    {
        public InvalidBasketIdException() : base(message: "Basket Id can not be empty.")
        {

        }
    }
}
