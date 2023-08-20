namespace Modules.Discounts.Domain.Exceptions
{
    public class InvalidDiscountValueException : Exception
    {
        public InvalidDiscountValueException() : base(message: "Discount value cannot be zero or negative.")
        {
            
        }
    }
}
