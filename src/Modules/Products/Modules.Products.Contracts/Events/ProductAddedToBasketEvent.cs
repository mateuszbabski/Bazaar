using Shared.Abstractions.Events;
using Shared.Domain.ValueObjects;

namespace Modules.Products.Contracts.Events
{
    public sealed record ProductAddedToBasketEvent(Guid ProductId,
                                            Guid ShopId,
                                            Guid CustomerId,
                                            MoneyValue ProductPrice,
                                            decimal ProductWeight,
                                            int Quantity) : IEvent
    {
    }
}
