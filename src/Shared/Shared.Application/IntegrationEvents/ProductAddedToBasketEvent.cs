using Shared.Abstractions.Events;
using Shared.Domain.ValueObjects;

namespace Modules.Shared.Application.IntegrationEvents
{
    public record ProductAddedToBasketEvent(Guid ProductId,
                                            Guid ShopId,
                                            Guid CustomerId,
                                            MoneyValue ProductPrice,
                                            int Quantity) : IEvent
    {
    }
}
