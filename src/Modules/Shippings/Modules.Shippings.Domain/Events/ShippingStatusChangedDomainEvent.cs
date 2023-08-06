using Shared.Domain;

namespace Modules.Shippings.Domain.Events
{
    public record ShippingStatusChangedDomainEvent(Guid Id) : IDomainEvent
    {
    }
}
