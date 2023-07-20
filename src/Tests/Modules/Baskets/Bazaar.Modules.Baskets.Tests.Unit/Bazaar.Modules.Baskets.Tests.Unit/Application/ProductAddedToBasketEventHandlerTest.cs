using Bazaar.Modules.Baskets.Tests.Unit.Domain;
using Modules.Baskets.Application.Events.EventHandlers;
using Modules.Baskets.Domain.Repositories;
using Modules.Shared.Application.IntegrationEvents;
using Moq;
using Shared.Abstractions.CurrencyConverters;
using Shared.Abstractions.UnitOfWork;
using Shared.Abstractions.UserServices;

namespace Bazaar.Modules.Baskets.Tests.Unit.Application
{
    public class ProductAddedToBasketEventHandlerTest
    {
        // TO DO
        private readonly ProductAddedToBasketEventHandler _sut;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly Mock<IBasketRepository> _basketRepositoryMock = new();
        private readonly Mock<ICurrencyConverter> _currencyConverterMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        public ProductAddedToBasketEventHandlerTest()
        {
            _sut = new ProductAddedToBasketEventHandler(_currentUserServiceMock.Object,
                                                        _basketRepositoryMock.Object,
                                                        _unitOfWorkMock.Object,
                                                        _currencyConverterMock.Object);
        }

        [Fact]
        public async Task ProductAddedEvent_CreatesAndAddProductToBasket_IfEventEmittedAndFetched()
        {
            var customerId = Guid.NewGuid();
            var product = BasketFactory.GetProduct();
            var basket = BasketFactory.CreateBasket(customerId);

            _currentUserServiceMock.Setup(x => x.UserId).Returns(customerId);
            _basketRepositoryMock.Setup(x => x.GetBasketByCustomerId(customerId)).ReturnsAsync(basket);
            _currencyConverterMock.Setup(x => x.GetConvertedPrice(product.Price.Amount,
                                                                  product.Price.Currency,
                                                                  basket.TotalPrice.Currency))
                .ReturnsAsync(product.Price.Amount);

            var productAddedEvent = new ProductAddedToBasketEvent(product.Id, product.ShopId, customerId, product.Price, 1);

            await _sut.Handle(productAddedEvent, CancellationToken.None);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(basket), Times.Once());
        }
    }
}
