using Modules.Shops.Domain.Entities;
using Shared.Domain;

namespace Modules.Shops.Domain.Events
{
    public sealed record ShopDetailsUpdatedDomainEvent(Shop Shop) : IDomainEvent
    {
    }
}
