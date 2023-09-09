using Shared.Domain;

namespace Modules.Shippings.Domain.Events
{
    public sealed record ShippingCreatedDomainEvent(Guid Id) : IDomainEvent
    {
    }
}
