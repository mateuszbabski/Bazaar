namespace Modules.Discounts.Domain.Exceptions
{
    public class InvalidDiscountCouponExpirationDateException : Exception
    {
        public InvalidDiscountCouponExpirationDateException() : base(message: "Coupon can't start after expiration date")
        {
            
        }
    }
}
