using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain;

namespace Modules.Discounts.Domain.Events
{
    public record NewDiscountCouponAddedToListDomainEvent(DiscountCode DiscountCode, DiscountTarget DiscountTarget) 
        : IDomainEvent
    {
    }
}
