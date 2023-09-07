using Shared.Abstractions.Events;

namespace Modules.Discounts.Contracts.Events
{
    public record DiscountCouponAddedForCustomerEvent(string DiscountCode, Guid? TargetId) : IEvent
    {
    }
}
