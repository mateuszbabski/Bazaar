using Modules.Orders.Domain.Events;
using Shared.Abstractions.DomainEvents;
using Shared.Abstractions.Events;
using Shared.Abstractions.Time;

namespace Modules.Orders.Application.Commands.CreateOrder
{
    internal class OrderCreatedDomainEventHandler : IDomainEventHandler<OrderCreatedDomainEvent>
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IDateTimeProvider _dateTimeProvider;

        public OrderCreatedDomainEventHandler(IEventDispatcher eventDispatcher, IDateTimeProvider dateTimeProvider)
        {
            _eventDispatcher = eventDispatcher;
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Order created successfully at: {@date}", _dateTimeProvider.UtcNow);
            // publish integration event to notify other services about the new order - create invoice, start payment process, etc.

            // notify shops about product list from the order that needs to be prepared

            // create an invoice for the order and send to customer

            //if (notification.DiscountTarget.DiscountType.ToString() == "customer")
            //{
            //    await _eventDispatcher.PublishAsync(new DiscountCouponAddedForCustomerEvent(notification.DiscountCode,
            //                                                                                notification.DiscountTarget.TargetId),
            //                                    cancellationToken);

            //}

            await Task.CompletedTask;
        }
    }
}
