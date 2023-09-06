using Modules.Discounts.Domain.Entities;
using Shared.Domain;

namespace Modules.Discounts.Domain.Events
{
    public record NewDiscountCouponAddedToListDomainEvent(DiscountCoupon DiscountCoupon) 
        : IDomainEvent
    {
    }
}
