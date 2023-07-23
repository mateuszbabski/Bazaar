using Modules.Products.Domain.Entities;
using Shared.Domain;
using Shared.Domain.ValueObjects;

namespace Modules.Products.Domain.Events
{
    public sealed record ProductPriceChangedDomainEvent(Guid Id, MoneyValue Price) : IDomainEvent
    {
    }
}
