namespace Modules.Discounts.Domain.Exceptions
{
    public class InvalidDiscountCouponException : Exception
    {
        public InvalidDiscountCouponException() : base(message: "Discount Coupon expired")
        {
            
        }
    }
}
