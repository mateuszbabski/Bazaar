using MediatR;

namespace Modules.Baskets.Application.Commands.CheckoutBasket
{
    public class CheckoutBasketCommand : IRequest<Unit>
    {
        //public string TelephoneNumber { get; set; }
        //public string Country { get; set; }
        //public string City { get; set; }
        //public string Street { get; set; }
        //public string PostalCode { get; set; }
        public string CouponCode { get; set; }
        public string ShippingMethod { get; set; }
        public int PaymentMethod { get; set; }
    }
}
