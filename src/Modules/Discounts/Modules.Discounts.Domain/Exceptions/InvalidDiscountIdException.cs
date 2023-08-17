namespace Modules.Discounts.Domain.Exceptions
{
    public class InvalidDiscountIdException : Exception
    {
        public InvalidDiscountIdException() : base(message: "Discount Id cannot be empty.")
        {

        }
    }
}
