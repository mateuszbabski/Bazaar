using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain;

namespace Modules.Discounts.Domain.Events
{
    public record NewDiscountCouponAddedToList(DiscountCouponId DiscountCouponId) : IDomainEvent
    {
    }
}
