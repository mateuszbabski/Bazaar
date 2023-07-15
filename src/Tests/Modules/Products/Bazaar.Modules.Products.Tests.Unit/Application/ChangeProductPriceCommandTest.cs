using Modules.Products.Application.Commands.ChangeProductPrice;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Exceptions;
using Modules.Products.Domain.Repositories;
using Modules.Shops.Domain.Repositories;
using Moq;
using Shared.Abstractions.UnitOfWork;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;
using Shared.Domain.Exceptions;

namespace Bazaar.Modules.Products.Tests.Unit.Application
{
    public class ChangeProductPriceCommandTest
    {
        private readonly ChangeProductPriceCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

        public ChangeProductPriceCommandTest()
        {
            _sut = new ChangeProductPriceCommandHandler(_currentUserServiceMock.Object,
                                                          _productRepositoryMock.Object,
                                                          _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task ChangeProductPrice_ChangesPriceIfValidMoneyValue()
        {
            var shop = ShopFactory.GetShop();

            var productList = new ProductListMock(shop);

            var command = new ChangeProductPriceCommand()
            {
                Id = productList.Products[0].Id,
                Amount = 5,
                Currency = "USD"
            };

            _currentUserServiceMock.Setup(s => s.UserId).Returns(shop.Id);

            _productRepositoryMock.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            Assert.Equal(10, productList.Products[0].Price.Amount);

            await _sut.Handle(command, CancellationToken.None);

            Assert.Equal(5, productList.Products[0].Price.Amount);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task ChangeProductPrice_ThrowsNotFoundIfIdIsInvalid()
        {
            var shop = ShopFactory.GetShop();

            var productList = new ProductListMock(shop);

            var command = new ChangeProductPriceCommand()
            {
                Id = Guid.NewGuid(),
                Amount = 5,
                Currency = "USD"
            };

            _currentUserServiceMock.Setup(s => s.UserId).Returns(shop.Id);

            var result = await Assert.ThrowsAsync<NotFoundException>(() 
                => _sut.Handle(command, CancellationToken.None));
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task ChangeProductPrice_ThrowsIfAmountIsZero()
        {
            var shop = ShopFactory.GetShop();

            var productList = new ProductListMock(shop);

            var command = new ChangeProductPriceCommand()
            {
                Id = productList.Products[0].Id,
                Amount = 0,
                Currency = "USD"
            };

            _currentUserServiceMock.Setup(s => s.UserId).Returns(shop.Id);

            _productRepositoryMock.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            var result = await Assert.ThrowsAsync<InvalidPriceException>(() 
                => _sut.Handle(command, CancellationToken.None));

            Assert.Equal("Money amount value cannot be zero or negative.", result.Message);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task ChangeProductPrice_ThrowsIfCurrencyIsNotAvailable()
        {
            var shop = ShopFactory.GetShop();

            var productList = new ProductListMock(shop);

            var command = new ChangeProductPriceCommand()
            {
                Id = productList.Products[0].Id,
                Amount = 5,
                Currency = "PES"
            };

            _currentUserServiceMock.Setup(s => s.UserId).Returns(shop.Id);

            _productRepositoryMock.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            var result = await Assert.ThrowsAsync<InvalidPriceException>(() 
                => _sut.Handle(command, CancellationToken.None));

            Assert.Equal("Invalid currency.", result.Message);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Never);
        }
    }
}
