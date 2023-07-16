using Modules.Baskets.Domain.Entities;
using Shared.Domain;

namespace Modules.Baskets.Domain.Events
{
    public sealed record ProductRemovedFromBasketDomainEvent(Basket Basket,
                                                             BasketItem BasketItem) 
        : IDomainEvent
    {
    }
}
