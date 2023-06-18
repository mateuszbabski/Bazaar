using Modules.Shops.Domain.Entities;
using Shared.Domain;

namespace Modules.Shops.Domain.Events
{
    public sealed record ShopCreatedDomainEvent(Shop Shop) : IDomainEvent
    {
    }
}
