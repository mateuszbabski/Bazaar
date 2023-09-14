using Shared.Abstractions.Events;
using Shared.Domain.ValueObjects;

namespace Modules.Products.Contracts.Events
{
    public sealed record ProductPriceChangedEvent(Guid Id,
                                           MoneyValue Price) : IEvent
    {
    }
}
