namespace Modules.Discounts.Domain.Exceptions
{
    public class InvalidDiscountCurrencyException : Exception
    {
        public InvalidDiscountCurrencyException() : base(message: "Discount has to have valid currency")
        {
            
        }
    }
}
