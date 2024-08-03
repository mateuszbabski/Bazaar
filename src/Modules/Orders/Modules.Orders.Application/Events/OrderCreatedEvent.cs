using Modules.Orders.Domain.Entities;
using Shared.Abstractions.Events;

namespace Modules.Orders.Application.Events
{
    public sealed record OrderCreatedEvent(Order Order) : IEvent
    {
    }
}
