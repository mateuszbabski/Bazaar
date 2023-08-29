using MediatR;
using Modules.Baskets.Application.Contracts;
using Modules.Baskets.Domain.Entities;
using Modules.Baskets.Domain.Repositories;
using Shared.Abstractions.Events;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;
using Modules.Baskets.Contracts.Events.BasketCheckedOut;

namespace Modules.Baskets.Application.Commands.CheckoutBasket
{
    public class CheckoutBasketCommandHandler : IRequestHandler<CheckoutBasketCommand, Unit>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketsUnitOfWork _unitOfWork;
        private readonly IEventDispatcher _eventDispatcher;

        public CheckoutBasketCommandHandler(ICurrentUserService currentUserService,
                                            IBasketRepository basketRepository,
                                            IBasketsUnitOfWork unitOfWork,
                                            IEventDispatcher eventDispatcher)
        {
            _currentUserService = currentUserService;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<Unit> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var customerId = _currentUserService.UserId;
            var basket = await _basketRepository.GetBasketByCustomerId(customerId)
                ?? throw new NotFoundException("Basket not found.");                       
            
            var basketMapped = CreateMappedBasket(basket);
            //var address = Address.CreateAddress(command.Country, command.City, command.Street, command.PostalCode);
            // TODO: after changing customer module
            var message = new BasketCheckoutMessage(basketMapped,
                                                    //command.TelephoneNumber,
                                                    //address,
                                                    command.CouponCode,
                                                    command.ShippingMethod,
                                                    command.PaymentMethod);

            await _eventDispatcher.PublishAsync(new BasketCheckedOutEvent(message), cancellationToken);

            //_basketRepository.DeleteBasket(basket);

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(basket);

            return Unit.Value;
        }

        private static BasketMapped CreateMappedBasket(Basket basket)
        {
            var itemList = new List<BasketItemMapped>();

            foreach (var basketItem in basket.Items)
            {
                var mappedItem = new BasketItemMapped
                {
                    Id = basketItem.Id,
                    BasketId = basketItem.BasketId,
                    ProductId = basketItem.ProductId,
                    ShopId = basketItem.ShopId,
                    Quantity = basketItem.Quantity,
                    Price = basketItem.Price,
                    BaseCurrencyPrice = basketItem.BaseCurrencyPrice
                };

                itemList.Add(mappedItem);
            }

            var basketMapped = new BasketMapped()
            {
                Id = basket.Id,
                CustomerId = basket.CustomerId,
                TotalPrice = basket.TotalPrice,
                Items = itemList
            };            

            return basketMapped;
        }
    }
}
