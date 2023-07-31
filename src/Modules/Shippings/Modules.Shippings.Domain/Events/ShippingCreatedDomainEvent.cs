using Shared.Domain;

namespace Modules.Shippings.Domain.Events
{
    public record ShippingCreatedDomainEvent(Guid Id) : IDomainEvent
    {
    }
}
