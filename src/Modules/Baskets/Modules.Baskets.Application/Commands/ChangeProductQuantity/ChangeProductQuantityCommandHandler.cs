using MediatR;
using Modules.Baskets.Application.Contracts;
using Modules.Baskets.Domain.Repositories;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Modules.Baskets.Application.Commands.ChangeProductQuantity
{
    public class ChangeProductQuantityCommandHandler : IRequestHandler<ChangeProductQuantityCommand, Unit>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketsUnitOfWork _unitOfWork;

        public ChangeProductQuantityCommandHandler(ICurrentUserService currentUserService,
                                                   IBasketRepository basketRepository,
                                                   IBasketsUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ChangeProductQuantityCommand command, CancellationToken cancellationToken)
        {
            var customerId = _currentUserService.UserId;

            var basket = await _basketRepository.GetBasketByCustomerId(customerId)
                ?? throw new NotFoundException("Basket not found.");

            basket.ChangeBasketItemQuantity(command.BasketItemId, command.Quantity);

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(basket);

            return Unit.Value;
        }
    }
}
