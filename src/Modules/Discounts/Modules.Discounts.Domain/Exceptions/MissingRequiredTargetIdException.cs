namespace Modules.Discounts.Domain.Exceptions
{
    public class MissingRequiredTargetIdException : Exception
    {
        public MissingRequiredTargetIdException() : base(message: "Chosen discount type needs target id")
        {
            
        }
    }
}
