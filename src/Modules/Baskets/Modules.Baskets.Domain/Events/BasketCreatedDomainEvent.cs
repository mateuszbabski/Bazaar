using Modules.Baskets.Domain.Entities;
using Shared.Domain;

namespace Modules.Baskets.Domain.Events
{
    public sealed record BasketCreatedDomainEvent(Basket Basket) : IDomainEvent
    {
    }
}
