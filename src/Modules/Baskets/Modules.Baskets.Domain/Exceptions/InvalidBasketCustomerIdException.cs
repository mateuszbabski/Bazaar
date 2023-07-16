namespace Modules.Baskets.Domain.Exceptions
{
    public class InvalidBasketCustomerIdException : Exception
    {
        public InvalidBasketCustomerIdException() : base(message: "Basket Customer Id is empty or invalid.")
        {

        }
    }
}
