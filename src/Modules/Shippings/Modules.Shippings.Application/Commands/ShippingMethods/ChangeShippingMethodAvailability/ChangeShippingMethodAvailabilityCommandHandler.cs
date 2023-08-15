using MediatR;
using Modules.Shippings.Domain.Repositories;
using Shared.Abstractions.UnitOfWork;
using Shared.Application.Exceptions;

namespace Modules.Shippings.Application.Commands.ShippingMethods.ChangeShippingMethodAvailability
{
    internal class ChangeShippingMethodAvailabilityCommandHandler : IRequestHandler<ChangeShippingMethodAvailabilityCommand, Unit>
    {
        private readonly IShippingMethodRepository _shippingMethodRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeShippingMethodAvailabilityCommandHandler(IShippingMethodRepository shippingMethodRepository,
                                                              IUnitOfWork unitOfWork)
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
