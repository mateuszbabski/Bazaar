using Modules.Products.Domain.Events;
using Shared.Abstractions.DomainEvents;
using Shared.Abstractions.Events;
using Shared.Application.IntegrationEvents;

namespace Modules.Products.Application.Commands.ChangeProductPrice
{
    public class ProductPriceChangedDomainEventHandler : IDomainEventHandler<ProductPriceChangedDomainEvent>
    {
        private readonly IEventDispatcher _eventDispatcher;

        public ProductPriceChangedDomainEventHandler(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }
        public async Task Handle(ProductPriceChangedDomainEvent notification, CancellationToken cancellationToken)
        {            
            await _eventDispatcher.PublishAsync(new ProductPriceChangedEvent(notification.Id, notification.Price),
                                                cancellationToken);            
        }
    }
}
