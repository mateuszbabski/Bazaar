using MediatR;
using Modules.Shippings.Domain.Repositories;
using Shared.Abstractions.UnitOfWork;
using Shared.Application.Exceptions;

namespace Modules.Shippings.Application.Commands.ShippingMethods.UpdateShippingMethodDetails
{
    internal class UpdateShippingMethodDetailsCommandHandler : IRequestHandler<UpdateShippingMethodDetailsCommand, Unit>
    {
        private readonly IShippingMethodRepository _shippingMethodRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateShippingMethodDetailsCommandHandler(IShippingMethodRepository shippingMethodRepository,
                                                         IUnitOfWork unitOfWork)
        {
            _shippingMethodRepository = shippingMethodRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateShippingMethodDetailsCommand command, CancellationToken cancellationToken)
        {
            var shippingMethod = await _shippingMethodRepository.GetShippingMethodById(command.Id)
                ?? throw new NotFoundException("Shipping method not found.");

            shippingMethod.UpdateDetails(command.Name,
                                         command.Amount,
                                         command.Currency,
                                         command.DurationTime);

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(shippingMethod);

            return Unit.Value;
        }
    }
}
