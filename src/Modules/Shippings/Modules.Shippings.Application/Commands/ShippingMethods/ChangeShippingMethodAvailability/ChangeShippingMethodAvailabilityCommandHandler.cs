using MediatR;
using Modules.Shippings.Application.Contracts;
using Modules.Shippings.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Modules.Shippings.Application.Commands.ShippingMethods.ChangeShippingMethodAvailability
{
    public class ChangeShippingMethodAvailabilityCommandHandler : IRequestHandler<ChangeShippingMethodAvailabilityCommand, Unit>
    {
        private readonly IShippingMethodRepository _shippingMethodRepository;
        private readonly IShippingMethodsUnitOfWork _unitOfWork;

        public ChangeShippingMethodAvailabilityCommandHandler(IShippingMethodRepository shippingMethodRepository,
                                                              IShippingMethodsUnitOfWork unitOfWork)
        {
            _shippingMethodRepository = shippingMethodRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ChangeShippingMethodAvailabilityCommand command, CancellationToken cancellationToken)
        {
            var shippingMethod = await _shippingMethodRepository.GetShippingMethodById(command.Id)
                ?? throw new NotFoundException("Shipping method not found.");

            shippingMethod.ChangeAvailability();

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(shippingMethod);

            return Unit.Value;
        }
    }
}
