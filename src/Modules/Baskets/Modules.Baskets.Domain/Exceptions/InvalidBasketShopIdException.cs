namespace Modules.Baskets.Domain.Exceptions
{
    public class InvalidBasketShopIdException : Exception
    {
        public InvalidBasketShopIdException() : base(message: "Basket Shop Id is empty or invalid.")
        {

        }
    }
}
