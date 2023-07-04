namespace Shared.Domain.Exceptions
{
    public class InvalidWeightException : Exception
    {
        public InvalidWeightException() : base(message: "Weight can't be equal or lower than 0.")
        {
            
        }
    }
}
