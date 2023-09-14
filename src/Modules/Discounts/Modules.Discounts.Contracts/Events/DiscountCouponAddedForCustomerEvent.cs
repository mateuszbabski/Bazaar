using Shared.Abstractions.Events;

namespace Modules.Discounts.Contracts.Events
{
    public sealed record DiscountCouponAddedForCustomerEvent(string DiscountCode,
                                                             Guid? TargetId) 
        : IEvent
    {
    }
}
