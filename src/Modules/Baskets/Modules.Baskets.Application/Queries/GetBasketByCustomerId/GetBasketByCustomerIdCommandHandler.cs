using MediatR;
using Modules.Baskets.Application.Dtos;
using Modules.Baskets.Domain.Repositories;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Modules.Baskets.Application.Queries.GetBasketByCustomerId
{
    public class GetBasketByCustomerIdCommandHandler : IRequestHandler<GetBasketByCustomerIdCommand, BasketDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IBasketRepository _basketRepository;

        public GetBasketByCustomerIdCommandHandler(ICurrentUserService currentUserService,
                                                   IBasketRepository basketRepository)
        {
            _currentUserService = currentUserService;
            _basketRepository = basketRepository;
        }
        public async Task<BasketDto> Handle(GetBasketByCustomerIdCommand command, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var basket = await _basketRepository.GetBasketByCustomerId(userId)
                ?? throw new NotFoundException("Basket not found.");

            var basketDto = BasketDto.CreateBasketDtoFromObject(basket);

            return basketDto;
        }
    }
}
