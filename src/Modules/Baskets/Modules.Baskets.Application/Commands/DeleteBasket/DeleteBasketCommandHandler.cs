using MediatR;
using Modules.Baskets.Application.Contracts;
using Modules.Baskets.Domain.Repositories;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Modules.Baskets.Application.Commands.DeleteBasket
{
    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, Unit>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketsUnitOfWork _unitOfWork;

        public DeleteBasketCommandHandler(ICurrentUserService currentUserService,
                                          IBasketRepository basketRepository,
                                          IBasketsUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var basket = await _basketRepository.GetBasketByCustomerId(userId)
                ?? throw new NotFoundException("Basket not found.");

            _basketRepository.DeleteBasket(basket);

            await _unitOfWork.CommitChangesAsync();
            return Unit.Value;
        }
    }
}
