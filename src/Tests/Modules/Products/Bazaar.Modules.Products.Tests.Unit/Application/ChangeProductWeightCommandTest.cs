using Modules.Products.Application.Commands.ChangeProductPrice;
using Modules.Products.Application.Commands.ChangeProductWeight;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Repositories;
using Moq;
using Shared.Abstractions.UnitOfWork;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;
using Shared.Domain.Exceptions;

namespace Bazaar.Modules.Products.Tests.Unit.Application
{
    public class ChangeProductWeightCommandTest
    {
        private readonly ChangeProductWeightCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

        public ChangeProductWeightCommandTest()
        {
            _sut = new ChangeProductWeightCommandHandler(_currentUserServiceMock.Object,
                                                         _productRepositoryMock.Object,
                                                         _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task ChangeProductWeight_ChangesWeightIsValid()
        {
            var shop = ShopFactory.GetShop();

            var productList = new ProductListMock(shop);

            var command = new ChangeProductWeightCommand()
            {
                Id = productList.Products[0].Id,
                WeightPerUnit = 5
            };

            _currentUserServiceMock.Setup(s => s.UserId).Returns(shop.Id);

            _productRepositoryMock.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            Assert.Equal(1, productList.Products[0].WeightPerUnit);

            await _sut.Handle(command, CancellationToken.None);

            Assert.Equal(5, productList.Products[0].WeightPerUnit);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task ChangeProductWeight_ThrowsNotFoundIfIdIsInvalid()
        {
            var shop = ShopFactory.GetShop();

            var productList = new ProductListMock(shop);

            var command = new ChangeProductWeightCommand()
            {
                Id = Guid.NewGuid(),
                WeightPerUnit = 5
            };

            _currentUserServiceMock.Setup(s => s.UserId).Returns(shop.Id);

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(command, CancellationToken.None));
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task ChangeProductWeight_ThrowsIfWeightIsZero()
        {
            var shop = ShopFactory.GetShop();

            var productList = new ProductListMock(shop);

            var command = new ChangeProductWeightCommand()
            {
                Id = productList.Products[0].Id,
                WeightPerUnit = 0
            };

            _currentUserServiceMock.Setup(s => s.UserId).Returns(shop.Id);

            _productRepositoryMock.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            var result = await Assert.ThrowsAsync<InvalidWeightException>(()
                => _sut.Handle(command, CancellationToken.None));

            Assert.Equal("Weight can't be equal or lower than 0.", result.Message);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Never);
        }
    }
}
