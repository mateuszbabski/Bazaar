using Modules.Orders.Domain.Entities;
using Shared.Domain;

namespace Modules.Orders.Domain.Events
{
    public sealed record OrderCreatedDomainEvent(Order Order) : IDomainEvent
    {
    }
}
