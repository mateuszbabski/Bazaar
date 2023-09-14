using Modules.Discounts.Domain.Entities;
using Shared.Domain;

namespace Modules.Discounts.Domain.Events
{
    public sealed record NewDiscountCreatedDomainEvent(Discount Discount) : IDomainEvent
    {
    }
}
