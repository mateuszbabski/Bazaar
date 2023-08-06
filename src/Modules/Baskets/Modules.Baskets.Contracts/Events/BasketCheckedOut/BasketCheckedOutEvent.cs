using Shared.Abstractions.Events;

namespace Modules.Baskets.Contracts.Events.BasketCheckedOut
{
    public record BasketCheckedOutEvent(BasketCheckoutMessage Message) : IEvent
    {
    }
}
