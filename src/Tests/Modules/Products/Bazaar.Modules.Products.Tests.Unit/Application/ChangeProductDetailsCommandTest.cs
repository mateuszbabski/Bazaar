using Modules.Products.Application.Commands.ChangeProductDetails;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Exceptions;
using Modules.Products.Domain.Repositories;
using Moq;
using Shared.Abstractions.UnitOfWork;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Bazaar.Modules.Products.Tests.Unit.Application
{
    public class ChangeProductDetailsCommandTest
    {
        private readonly ChangeProductDetailsCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

        public ChangeProductDetailsCommandTest()
        {
            _sut = new ChangeProductDetailsCommandHandler(_currentUserServiceMock.Object,
                                                          _productRepositoryMock.Object,
                                                          _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task UpdateProduct_UpdatesProductIfValidParams()
        {
            var shop = ShopFactory.GetShop();

            var productList = new ProductListMock(shop);

            var command = new ChangeProductDetailsCommand()
            {
                Id = productList.Products[0].Id,
                ProductName = "Updated name",
                ProductDescription = "Updated description",
                Unit = "pcs"
            };
            _currentUserServiceMock.Setup(s => s.UserId).Returns(shop.Id);

            _productRepositoryMock.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            await _sut.Handle(command, CancellationToken.None);

            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Once);

            Assert.Equal("Updated name", productList.Products[0].ProductName);
            Assert.Equal("Updated description", productList.Products[0].ProductDescription);
            Assert.Equal("pcs", productList.Products[0].Unit);
        }

        [Fact]
        public async Task UpdateProduct_DoNothingIfParamsAreEmpty()
        {
            var shop = ShopFactory.GetShop();

            var productList = new ProductListMock(shop);

            var command = new ChangeProductDetailsCommand()
            {
                Id = productList.Products[0].Id
            };
            _currentUserServiceMock.Setup(s => s.UserId).Returns(shop.Id);

            _productRepositoryMock.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            await _sut.Handle(command, CancellationToken.None);

            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Once);
            Assert.Equal("productName", productList.Products[0].ProductName);
        }

        [Fact]
        public async Task UpdateProduct_ThrowsNotFoundIfIdIsInvalid()
        {
            var shop = ShopFactory.GetShop();

            var productList = new ProductListMock(shop);

            var command = new ChangeProductDetailsCommand()
            {
                Id = Guid.NewGuid()
            };
            _currentUserServiceMock.Setup(s => s.UserId).Returns(shop.Id);

            var result = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task UpdateProduct_ThrowsNotFoundIfCategoryParamIsInvalid()
        {
            var shop = ShopFactory.GetShop();

            var productList = new ProductListMock(shop);

            var command = new ChangeProductDetailsCommand()
            {
                Id = productList.Products[0].Id,
                ProductCategory = "UFO"
            };
            _currentUserServiceMock.Setup(s => s.UserId).Returns(shop.Id);
            _productRepositoryMock.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            var result = await Assert.ThrowsAsync<InvalidProductCategoryException>(() 
                => _sut.Handle(command, CancellationToken.None));

            Assert.IsType<InvalidProductCategoryException>(result);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Never);
        }
    }
}
