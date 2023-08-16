using MediatR;
using Modules.Shippings.Application.Contracts;
using Modules.Shippings.Domain.Entities;
using Modules.Shippings.Domain.Repositories;
using Shared.Domain.ValueObjects;

namespace Modules.Shippings.Application.Commands.ShippingMethods.AddShippingMethod
{
    public class AddShippingMethodCommandHandler : IRequestHandler<AddShippingMethodCommand, Guid>
    {
        private readonly IShippingMethodRepository _shippingMethodRepository;
        private readonly IShippingMethodsUnitOfWork _unitOfWork;

        public AddShippingMethodCommandHandler(IShippingMethodRepository shippingMethodRepository,
                                               IShippingMethodsUnitOfWork unitOfWork)
        {
            _shippingMethodRepository = shippingMethodRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AddShippingMethodCommand command, CancellationToken cancellationToken)
        {
            var price = MoneyValue.Of(command.Amount, command.Currency);

            var shippingMethod = ShippingMethod.CreateNewShippingMethod(command.Name,
                                                                        price,
                                                                        command.DurationInDays);

            await _shippingMethodRepository.AddShippingMethod(shippingMethod);

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(shippingMethod);

            return shippingMethod.Id;
        }
    }
}
