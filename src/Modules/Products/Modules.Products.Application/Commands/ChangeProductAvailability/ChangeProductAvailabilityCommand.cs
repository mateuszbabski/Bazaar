using MediatR;

namespace Modules.Products.Application.Commands.ChangeProductAvailability
{
    public class ChangeProductAvailabilityCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
