namespace Modules.Baskets.Domain.Exceptions
{
    public class BasketItemNotFoundException : Exception
    {
        public BasketItemNotFoundException() : base(message: "Basket item not found.")
        {
            
        }
    }
}
