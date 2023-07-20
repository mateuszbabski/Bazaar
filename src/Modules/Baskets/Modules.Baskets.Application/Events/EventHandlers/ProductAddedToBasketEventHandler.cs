using Modules.Baskets.Application.Contracts;
using Modules.Baskets.Application.Exceptions;
using Modules.Baskets.Domain.Entities;
using Modules.Baskets.Domain.Repositories;
using Modules.Shared.Application.IntegrationEvents;
using Shared.Abstractions.CurrencyConverters;
using Shared.Abstractions.Events;
using Shared.Abstractions.UserServices;

namespace Modules.Baskets.Application.Events.EventHandlers
{
    public class ProductAddedToBasketEventHandler : IEventHandler<ProductAddedToBasketEvent>
    {
        private readonly ICurrentUserService _userService;
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketsUnitOfWork _unitOfWork;
        private readonly ICurrencyConverter _currencyConverter;

        public ProductAddedToBasketEventHandler(ICurrentUserService userService,
                                                IBasketRepository basketRepository,
                                                IBasketsUnitOfWork unitOfWork,
                                                ICurrencyConverter currencyConverter)
        {
            _userService = userService;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _currencyConverter = currencyConverter;
        }
        public async Task Handle(ProductAddedToBasketEvent notification, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;

            if(userId != notification.CustomerId)
            {
                throw new InvalidUserException("Customer Id conflict.");
            }

            var basket = await ReturnOrCreateNewBasket(notification.CustomerId, notification.ProductPrice.Currency);

            var productConvertedPrice = await _currencyConverter.GetConvertedPrice(notification.ProductPrice.Amount,
                                                                                   notification.ProductPrice.Currency,
                                                                                   basket.TotalPrice.Currency);

            basket.AddProductToBasket(notification.ProductId,
                                      notification.ShopId,
                                      notification.Quantity,
                                      notification.ProductPrice,
                                      productConvertedPrice);            

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(basket);
        }

        private async Task<Basket> ReturnOrCreateNewBasket(Guid customerId, string currency)
        {
            var basket = await _basketRepository.GetBasketByCustomerId(customerId);

            if (basket == null)
            {
                var newBasket = Basket.CreateBasket(customerId, currency);
                await _basketRepository.CreateBasket(newBasket);
                return newBasket;
            }

            return basket;
        }
    }
}
