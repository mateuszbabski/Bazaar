using Bazaar.Modules.Baskets.Tests.Unit.Domain;
using Modules.Baskets.Application.Commands.ChangeBasketCurrency;
using Modules.Baskets.Domain.Entities;
using Modules.Baskets.Domain.Exceptions;
using Modules.Baskets.Domain.Repositories;
using Modules.Products.Domain.Exceptions;
using Modules.Products.Domain.Repositories;
using Moq;
using Shared.Abstractions.CurrencyConverters;
using Shared.Abstractions.UnitOfWork;
using Shared.Abstractions.UserServices;

namespace Bazaar.Modules.Baskets.Tests.Unit.Application
{
    public class ChangeBasketCurrencyCommandTest
    {
        private readonly ChangeBasketCurrencyCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly Mock<IBasketRepository> _basketRepositoryMock = new();
        private readonly Mock<ICurrencyConverter> _currencyConverterMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

        public ChangeBasketCurrencyCommandTest()
        {
            _sut = new ChangeBasketCurrencyCommandHandler(_currentUserServiceMock.Object,
                                                          _basketRepositoryMock.Object,
                                                          _currencyConverterMock.Object,
                                                          _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task ChangeBasketCurrency_ChangesCurrency_IfSystemAcceptsIt()
        {
            var userId = Guid.NewGuid();
            var command = new ChangeBasketCurrencyCommand()
            {
                Currency = "PLN"
            };

            var basket = CreateBasketWithProduct(userId);

            _currentUserServiceMock.Setup(x => x.UserId).Returns(userId);
            _basketRepositoryMock.Setup(x => x.GetBasketByCustomerId(userId)).ReturnsAsync(basket);
            _currencyConverterMock.Setup(x => x.GetConversionRate(basket.TotalPrice.Currency, command.Currency)).ReturnsAsync(4.0M);

            var result = await _sut.Handle(command, CancellationToken.None);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(basket), Times.Once);
        }

        [Fact]
        public async Task ChangeBasketCurrency_Throws_IfSystemDoesntAcceptCurrency()
        {
            var userId = Guid.NewGuid();
            var command = new ChangeBasketCurrencyCommand()
            {
                Currency = "PES"
            };

            var basket = CreateBasketWithProduct(userId);

            _currentUserServiceMock.Setup(x => x.UserId).Returns(userId);
            _basketRepositoryMock.Setup(x => x.GetBasketByCustomerId(userId)).ReturnsAsync(basket);

            var result = await Assert.ThrowsAsync<InvalidBasketPriceException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal("Invalid currency.", result.Message);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(basket), Times.Never);
        }

        [Fact]
        public async Task ChangeBasketCurrency_ReturnsBasketId_IfCommandCurrencyTheSameAsBasketCurrency()
        {
            var userId = Guid.NewGuid();
            var basket = CreateBasketWithProduct(userId);

            var command = new ChangeBasketCurrencyCommand()
            {
                Currency = basket.TotalPrice.Currency
            };

            _currentUserServiceMock.Setup(x => x.UserId).Returns(userId);
            _basketRepositoryMock.Setup(x => x.GetBasketByCustomerId(userId)).ReturnsAsync(basket);

            var result = await _sut.Handle(command, CancellationToken.None);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(basket), Times.Never);

            Assert.Equal(basket.Id.Value, result);
        }

        private Basket CreateBasketWithProduct(Guid customerId)
        {
            var basket = Basket.CreateBasket(customerId, "USD");
            var product = BasketFactory.GetProduct();

            basket.AddProductToBasket(product.Id, product.ShopId, 1, product.Price, product.Price.Amount);

            return basket;
        }
    }
}
