using Bazaar.Modules.Baskets.Tests.Unit.Domain;
using Modules.Baskets.Application.Commands.ChangeProductQuantity;
using Modules.Baskets.Application.Contracts;
using Modules.Baskets.Domain.Entities;
using Modules.Baskets.Domain.Repositories;
using Moq;
using Shared.Abstractions.UserServices;

namespace Bazaar.Modules.Baskets.Tests.Unit.Application
{
    public class ChangeProductQuantityCommandTest
    {
        private readonly ChangeProductQuantityCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly Mock<IBasketRepository> _basketRepositoryMock = new();
        private readonly Mock<IBasketsUnitOfWork> _unitOfWorkMock = new();
        public ChangeProductQuantityCommandTest()
        {
            _sut = new ChangeProductQuantityCommandHandler(_currentUserServiceMock.Object,
                                                           _basketRepositoryMock.Object,
                                                           _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task ChangeProductQuantity_ChangesQuantity_IfQuantityMoreThanZero()
        {
            var userId = Guid.NewGuid();
            var basket = CreateBasketWithProduct(userId);

            var command = new ChangeProductQuantityCommand()
            {
                BasketItemId = basket.Items.First().Id,
                Quantity = 2                
            };

            _currentUserServiceMock.Setup(x => x.UserId).Returns(userId);
            _basketRepositoryMock.Setup(x => x.GetBasketByCustomerId(userId)).ReturnsAsync(basket);

            var result = await _sut.Handle(command, CancellationToken.None);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(basket), Times.Once);

            Assert.Equal(2, basket.Items.First().Quantity);
        }

        private static Basket CreateBasketWithProduct(Guid customerId)
        {
            var basket = Basket.CreateBasket(customerId, "USD");
            var product = BasketFactory.GetProduct();

            basket.AddProductToBasket(product.Id, product.ShopId, 1, product.Price, product.WeightPerUnit, product.Price.Amount);

            return basket;
        }
    }
}
