namespace Modules.Orders.Domain.Exceptions
{
    internal class InvalidOrderIdException : Exception
    {
        public InvalidOrderIdException() : base(message: "Order Id cannot be empty.")
        {

        }
    }
}
