using MediatR;

namespace Modules.Shippings.Application.Commands.ShippingMethods.ChangeShippingMethodAvailability
{
    public class ChangeShippingMethodAvailabilityCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
