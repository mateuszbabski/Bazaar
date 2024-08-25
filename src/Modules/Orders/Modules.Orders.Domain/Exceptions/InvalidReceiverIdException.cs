namespace Modules.Orders.Domain.Exceptions
{
    internal class InvalidReceiverIdException : Exception
    {
        public InvalidReceiverIdException() : base(message: "Receiver Id cannot be empty.")
        {

        }
    }
}
