using MediatR;
using Modules.Baskets.Domain.Repositories;
using Shared.Abstractions.UnitOfWork;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Modules.Baskets.Application.Commands.RemoveProductFromBasket
{
    public class RemoveProductFromBasketCommandHandler : IRequestHandler<RemoveProductFromBasketCommand, Unit>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveProductFromBasketCommandHandler(ICurrentUserService currentUserService,
                                                     IBasketRepository basketRepository,
                                                     IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RemoveProductFromBasketCommand command, CancellationToken cancellationToken)
        {
            var customerId = _currentUserService.UserId;
            var basket = await _basketRepository.GetBasketByCustomerId(customerId)
                ?? throw new NotFoundException("Cart not found.");

            basket.RemoveItemFromBasket(command.Id);

            if (basket.Items.Count == 0)
            {
                _basketRepository.DeleteBasket(basket);
            }

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(basket);

            return Unit.Value;
        }
    }
}
