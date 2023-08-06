using Bazaar.Modules.Baskets.Tests.Unit.Domain;
using Modules.Baskets.Application.Commands.RemoveProductFromBasket;
using Modules.Baskets.Application.Contracts;
using Modules.Baskets.Domain.Entities;
using Modules.Baskets.Domain.Repositories;
using Moq;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Bazaar.Modules.Baskets.Tests.Unit.Application
{
    public class RemoveProductFromBasketCommandTest
    {
        private readonly RemoveProductFromBasketCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly Mock<IBasketRepository> _basketRepositoryMock = new();
        private readonly Mock<IBasketsUnitOfWork> _unitOfWorkMock = new();

        public RemoveProductFromBasketCommandTest()
        {
            _sut = new RemoveProductFromBasketCommandHandler(_currentUserServiceMock.Object,
                                                             _basketRepositoryMock.Object,
                                                             _unitOfWorkMock.Object);
        }

        [Fact]
        public async void RemoveProductFromBasket_RemovesBasket_WhenLastProductIsRemoved()
        {
            var customerId = Guid.NewGuid();
            var basket = CreateBasketWithProducs(customerId);

            var command = new RemoveProductFromBasketCommand()
            {
                Id = basket.Items[0].Id,
            };

            var command2 = new RemoveProductFromBasketCommand()
            {
                Id = basket.Items[1].Id,
            };

            _currentUserServiceMock.Setup(x => x.UserId).Returns(customerId);
            _basketRepositoryMock.Setup(x => x.GetBasketByCustomerId(customerId)).ReturnsAsync(basket);

            await _sut.Handle(command2, CancellationToken.None);
            await _sut.Handle(command, CancellationToken.None);

            _basketRepositoryMock.Setup(x => x.DeleteBasket(basket)).Equals(true);

            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(basket), Times.Exactly(2));
            _basketRepositoryMock.Verify(x => x.DeleteBasket(basket), Times.Once);
        }

        [Fact]
        public async void RemoveProductFromBasket_RemovesProduct_IfExists()
        {
            var customerId = Guid.NewGuid();
            var basket = CreateBasketWithProducs(customerId);

            var command = new RemoveProductFromBasketCommand()
            {
                Id = basket.Items[0].Id,
            };

            _currentUserServiceMock.Setup(x => x.UserId).Returns(customerId);
            _basketRepositoryMock.Setup(x => x.GetBasketByCustomerId(customerId)).ReturnsAsync(basket);

            await _sut.Handle(command, CancellationToken.None);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Basket>()), Times.Once);
            Assert.Single(basket.Items);
        }

        [Fact]
        public async void RemoveProductFromBasket_ThrowsException_IfBasketDoesntExist()
        {
            var customerId = Guid.NewGuid();

            var command = new RemoveProductFromBasketCommand()
            {
                Id = Guid.NewGuid()
            };

            _currentUserServiceMock.Setup(s => s.UserId).Returns(customerId);
            _basketRepositoryMock.Setup(x => x.GetBasketByCustomerId(customerId)).ThrowsAsync(new NotFoundException("Basket not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));

            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Basket>()), Times.Never);
            Assert.Equal("Basket not found.", result.Message);
        }

        private static Basket CreateBasketWithProducs(Guid customerId)
        {
            var basket = Basket.CreateBasket(customerId, "USD");
            var product = BasketFactory.GetProduct();
            var product2 = BasketFactory.GetProduct();

            basket.AddProductToBasket(product.Id, product.ShopId, 1, product.Price, product.WeightPerUnit, product.Price.Amount);
            basket.AddProductToBasket(product2.Id, product2.ShopId, 2, product2.Price, product2.WeightPerUnit, product2.Price.Amount);

            return basket;
        }
    }
}
