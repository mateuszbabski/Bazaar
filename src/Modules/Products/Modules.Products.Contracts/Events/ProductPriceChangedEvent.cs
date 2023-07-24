using Shared.Abstractions.Events;
using Shared.Domain.ValueObjects;

namespace Modules.Products.Contracts.Events
{
    public record ProductPriceChangedEvent(Guid Id,
                                           MoneyValue Price) : IEvent
    {
    }
}
