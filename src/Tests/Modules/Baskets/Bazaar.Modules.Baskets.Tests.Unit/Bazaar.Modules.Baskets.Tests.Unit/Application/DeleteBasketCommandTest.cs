using Modules.Baskets.Application.Commands.DeleteBasket;
using Modules.Baskets.Domain.Entities;
using Modules.Baskets.Domain.Repositories;
using Moq;
using Shared.Abstractions.UnitOfWork;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Bazaar.Modules.Baskets.Tests.Unit.Application
{
    public class DeleteBasketCommandTest
    {
        private readonly DeleteBasketCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly Mock<IBasketRepository> _basketRepositoryMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

        public DeleteBasketCommandTest()
        {
            _sut = new DeleteBasketCommandHandler(_currentUserServiceMock.Object,
                                                  _basketRepositoryMock.Object,
                                                  _unitOfWorkMock.Object);    
        }

        [Fact]
        public async void DeleteBasket_DeletesCart_IfExists()
        {
            var customerId = Guid.NewGuid();

            var command = new DeleteBasketCommand();

            var basket = Basket.CreateBasket(customerId, "USD");            

            _currentUserServiceMock.Setup(s => s.UserId).Returns(customerId);
            _basketRepositoryMock.Setup(x => x.GetBasketByCustomerId(customerId)).ReturnsAsync(basket);
            _basketRepositoryMock.Setup(x => x.DeleteBasket(basket)).Equals(true);

            await _sut.Handle(command, CancellationToken.None);

            _unitOfWorkMock.Verify(x => x.CommitChangesAsync(), Times.Once);
        }

        [Fact]
        public async void DeleteBasket_ThrowsNotFoundException_IfDoesntExist()
        {
            var customerId = Guid.NewGuid();

            var command = new DeleteBasketCommand();

            _currentUserServiceMock.Setup(s => s.UserId).Returns(customerId);
            _basketRepositoryMock.Setup(x => x.GetBasketByCustomerId(customerId)).ThrowsAsync(new NotFoundException("Basket not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));

            _unitOfWorkMock.Verify(x => x.CommitChangesAsync(), Times.Never);
            Assert.Equal("Basket not found.", result.Message);
        }
    }
}
