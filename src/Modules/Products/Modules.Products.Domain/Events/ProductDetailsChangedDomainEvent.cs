using Modules.Products.Domain.Entities;
using Shared.Domain;

namespace Modules.Products.Domain.Events
{
    public sealed record ProductDetailsChangedDomainEvent(Product Product) : IDomainEvent
    {
    }
}
