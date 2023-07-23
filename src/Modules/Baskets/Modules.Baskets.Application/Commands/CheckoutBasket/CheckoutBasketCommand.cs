using MediatR;

namespace Modules.Baskets.Application.Commands.CheckoutBasket
{
    public class CheckoutBasketCommand : IRequest<Unit>
    {
        public string CouponCode { get; set; }
        public int ShippingMethod { get; set; }
        public int PaymentMethod { get; set; }
    }
}
