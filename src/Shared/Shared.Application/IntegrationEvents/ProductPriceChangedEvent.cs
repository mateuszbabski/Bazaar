using Shared.Abstractions.Events;
using Shared.Domain.ValueObjects;

namespace Shared.Application.IntegrationEvents
{
    public record ProductPriceChangedEvent(Guid ProductId,
                                           MoneyValue ProductPrice) : IEvent
    {
    }
}
