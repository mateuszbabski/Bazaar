using Modules.Discounts.Contracts.Events;
using Modules.Discounts.Domain.Events;
using Shared.Abstractions.DomainEvents;
using Shared.Abstractions.Events;

namespace Modules.Discounts.Application.Commands.DiscountCoupons.CreateDiscountCoupon
{
    internal class NewDiscountCouponAddedToListDomainEventHandler : IDomainEventHandler<NewDiscountCouponAddedToListDomainEvent>
    {
        private readonly IEventDispatcher _eventDispatcher;

        public NewDiscountCouponAddedToListDomainEventHandler(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }
        public async Task Handle(NewDiscountCouponAddedToListDomainEvent notification, CancellationToken cancellationToken)
        {
            if(notification.DiscountTarget.DiscountType.ToString() == "customer")
            {
                await _eventDispatcher.PublishAsync(new DiscountCouponAddedForCustomerEvent(notification.DiscountCode,
                                                                                            notification.DiscountTarget.TargetId),
                                                cancellationToken);

            }

            await Task.CompletedTask;
        }
    }
}
