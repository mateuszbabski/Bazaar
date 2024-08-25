namespace Modules.Orders.Domain.Exceptions
{
    internal class InvalidOrderItemIdException : Exception
    {
        public InvalidOrderItemIdException() : base(message: "Order Item Id cannot be empty.")
        {

        }
    }
}
