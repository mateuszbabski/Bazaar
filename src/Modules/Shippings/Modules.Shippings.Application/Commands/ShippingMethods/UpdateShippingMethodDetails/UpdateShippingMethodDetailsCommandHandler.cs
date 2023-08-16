using MediatR;
using Modules.Shippings.Application.Contracts;
using Modules.Shippings.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Modules.Shippings.Application.Commands.ShippingMethods.UpdateShippingMethodDetails
{
    public class UpdateShippingMethodDetailsCommandHandler : IRequestHandler<UpdateShippingMethodDetailsCommand, Unit>
    {
        private readonly IShippingMethodRepository _shippingMethodRepository;
        private readonly IShippingMethodsUnitOfWork _unitOfWork;

        public UpdateShippingMethodDetailsCommandHandler(IShippingMethodRepository shippingMethodRepository,
                                                         IShippingMethodsUnitOfWork unitOfWork)
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
