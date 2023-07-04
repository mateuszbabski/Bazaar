using Modules.Products.Domain.Entities;
using Shared.Domain;

namespace Modules.Products.Domain.Events
{
    public record ProductPriceUpdatedDomainEvent(Product Product) : IDomainEvent
    {
    }
}
