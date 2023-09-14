using Shared.Domain;

namespace Modules.Shippings.Domain.Events
{
    public sealed record ShippingStatusChangedDomainEvent(Guid Id) : IDomainEvent
    {
    }
}
