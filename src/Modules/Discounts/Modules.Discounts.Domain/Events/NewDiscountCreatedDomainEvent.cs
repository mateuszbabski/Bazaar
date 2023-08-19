using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain;

namespace Modules.Discounts.Domain.Events
{
    public record NewDiscountCreatedDomainEvent(DiscountId DiscountId) : IDomainEvent
    {
    }
}
