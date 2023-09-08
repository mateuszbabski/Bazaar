using Shared.Domain.ValueObjects;

namespace Modules.Baskets.Contracts.Events.BasketCheckedOut
{
    public class BasketCheckoutMessage
    {
        public Guid Id { get; }
        public DateTimeOffset OccuredOn { get; }
        public Guid BasketId { get; }
        public Guid CustomerId { get; }
        public MoneyValue TotalPrice { get; }
        public List<BasketItemMapped> BasketItems { get; }
        public string CouponCode { get; }
        public int ShippingMethod { get; }
        public int PaymentMethod { get; }
        public BasketCheckoutMessage(BasketMapped basketMapped,
                                     string couponCode,
                                     int shippingMethod,
                                     int paymentMethod)
        {
            this.Id = Guid.NewGuid();
            this.OccuredOn = DateTimeOffset.UtcNow;
            this.BasketId = basketMapped.Id;
            this.CustomerId = basketMapped.CustomerId;
            this.TotalPrice = basketMapped.TotalPrice;
            this.BasketItems = basketMapped.Items;
            this.CouponCode = couponCode;
            this.ShippingMethod = shippingMethod;
            this.PaymentMethod = paymentMethod;
        }
    }
}
