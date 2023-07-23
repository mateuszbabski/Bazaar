using Shared.Abstractions.Events;

namespace Shared.Application.IntegrationEvents.BasketCheckedOut
{
    public record BasketCheckedOutEvent(BasketCheckoutMessage Message) : IEvent
    {
    }

}
