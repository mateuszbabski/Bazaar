using Modules.Products.Application.Commands.AddProduct;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Exceptions;
using Modules.Products.Domain.Repositories;
using Moq;
using Shared.Abstractions.UnitOfWork;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;
using Shared.Domain.Exceptions;

namespace Bazaar.Modules.Products.Tests.Unit.Application
{
    public class AddProductCommandTest
    {
        private readonly AddProductCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

        public AddProductCommandTest()
        {
            _sut = new AddProductCommandHandler(_currentUserServiceMock.Object,
                                                _productRepositoryMock.Object,
                                                _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddProduct_ValidParams_ShouldReturnProductId()
        {
            var shopId = Guid.NewGuid();
            var command = new AddProductCommand()
            {
                ProductName = "productName",
                ProductDescription = "productDescription",
                ProductCategory = "Food",
                Amount = 10,
                Currency = "PLN",
                WeightPerUnit = 1,
                Unit = "piece"
            };

            _currentUserServiceMock.Setup(x => x.UserId).Returns(shopId);
            
            var result = await _sut.Handle(command, CancellationToken.None);

            _productRepositoryMock.Verify(x => x.Add(It.Is<Product>(x => x.Id.Value == result)), Times.Once);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Once);

            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task AddProduct_InValidShopIdParam_ShouldThrowException()
        {
            var command = new AddProductCommand()
            {
                ProductName = "productName",
                ProductDescription = "productDescription",
                ProductCategory = "Food",
                Amount = 10,
                Currency = "PLN",
                WeightPerUnit = 1,
                Unit = "piece"
            };

            _currentUserServiceMock.Setup(x => x.UserId).Returns(Guid.Empty);

            var act = await Assert.ThrowsAsync<ForbidException>(() => _sut.Handle(command, CancellationToken.None));

            _productRepositoryMock.Verify(x => x.Add(It.IsAny<Product>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Never);

            Assert.IsType<ForbidException>(act);
        }

        [Fact]
        public async Task AddProduct_InValidParams_ShouldThrowException()
        {
            var shopId = Guid.NewGuid();
            var command = new AddProductCommand()
            {
                ProductName = "",
                ProductDescription = "productDescription",
                ProductCategory = "Food",
                Amount = 10,
                Currency = "PLN",
                WeightPerUnit = 1,
                Unit = "piece"
            };

            _currentUserServiceMock.Setup(x => x.UserId).Returns(shopId);

            var act = await Assert.ThrowsAsync<InvalidProductNameException>(() => _sut.Handle(command, CancellationToken.None));

            _productRepositoryMock.Verify(x => x.Add(It.IsAny<Product>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Never);

            Assert.IsType<InvalidProductNameException>(act);
            Assert.Equal("Product name cannot be empty.", act.Message);
        }

        [Fact]
        public async Task AddProduct_InValidPriceParam_ShouldThrowException()
        {
            var shopId = Guid.NewGuid();
            var command = new AddProductCommand()
            {
                ProductName = "productName",
                ProductDescription = "productDescription",
                ProductCategory = "Food",
                Amount = 0,
                Currency = "PLN",
                WeightPerUnit = 1,
                Unit = "piece"
            };

            _currentUserServiceMock.Setup(x => x.UserId).Returns(shopId);

            var act = await Assert.ThrowsAsync<InvalidPriceException>(() => _sut.Handle(command, CancellationToken.None));

            _productRepositoryMock.Verify(x => x.Add(It.IsAny<Product>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Never);

            Assert.IsType<InvalidPriceException>(act);
            Assert.Equal("Money amount value cannot be zero or negative.", act.Message);
        }

        [Fact]
        public async Task AddProduct_InValidCategoryParam_ShouldThrowException()
        {
            var shopId = Guid.NewGuid();
            var command = new AddProductCommand()
            {
                ProductName = "productName",
                ProductDescription = "productDescription",
                ProductCategory = "",
                Amount = 10,
                Currency = "PLN",
                WeightPerUnit = 1,
                Unit = "piece"
            };

            _currentUserServiceMock.Setup(x => x.UserId).Returns(shopId);

            var act = await Assert.ThrowsAsync<InvalidProductCategoryException>(() => _sut.Handle(command, CancellationToken.None));

            _productRepositoryMock.Verify(x => x.Add(It.IsAny<Product>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Product>()), Times.Never);

            Assert.IsType<InvalidProductCategoryException>(act);
            Assert.Equal("Invalid product category.", act.Message);
        }
    }
}
