using Modules.Baskets.Contracts.Events.BasketCheckedOut;
using Modules.Discounts.Contracts.Interfaces;
using Modules.Discounts.Domain.Entities;
using Modules.Shippings.Contracts;
using Shared.Abstractions.Events;
using Shared.Application.Exceptions;

namespace Modules.Orders.Application.Events.EventHandlers
{
    public class BasketCheckedOutEventHandler : IEventHandler<BasketCheckedOutEvent>
    {
        private readonly IDiscountChecker _discountChecker;
        private readonly IShippingMethodChecker _shippingMethodChecker;

        public BasketCheckedOutEventHandler(IDiscountChecker discountChecker, 
                                            IShippingMethodChecker shippingMethodChecker)
        {
            _discountChecker = discountChecker;
            _shippingMethodChecker = shippingMethodChecker;
        }

        public async Task Handle(BasketCheckedOutEvent notification, CancellationToken cancellationToken)
        {
            var shippingMethod = await _shippingMethodChecker.GetShippingMethodByItsName(notification.Message.ShippingMethod);
            if (shippingMethod == null)
            {
                throw new NotFoundException(notification.Message.ShippingMethod);
            }

            var discount = await _discountChecker.GetDiscountByCouponCodeToProcess(notification.Message.CouponCode);
            if (discount != null)
            {
                ProcessDiscount(discount, notification.Message);
            }
            // mock paymentmethod until module is done
            // create updated order
            // emit event or call order domain to create an order
        }

        private void ProcessDiscount(Discount discount, BasketCheckoutMessage message)
        {
            // check discout type, use switch method to process discount across the basket, update price
            
        }
    }
}
