using Modules.Discounts.Domain.Events;
using Shared.Abstractions.DomainEvents;

namespace Modules.Discounts.Application.Commands.DiscountCoupons.CreateDiscountCoupon
{
    internal class NewDiscountCouponAddedToListDomainEventHandler : IDomainEventHandler<NewDiscountCouponAddedToListDomainEvent>
    { // TODO: send users email if coupon for him dont use integration event - no logic
        public NewDiscountCouponAddedToListDomainEventHandler()
        {
            
        }
        public async Task Handle(NewDiscountCouponAddedToListDomainEvent notification, CancellationToken cancellationToken)
        {
            // Contract to get customers email and send it
            throw new NotImplementedException();
        }
    }
}
