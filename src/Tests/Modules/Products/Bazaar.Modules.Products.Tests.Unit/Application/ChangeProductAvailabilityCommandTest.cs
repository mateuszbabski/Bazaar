using Modules.Products.Application.Commands.ChangeProductAvailability;
using Modules.Products.Domain.Repositories;
using Moq;
using Shared.Abstractions.UnitOfWork;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Bazaar.Modules.Products.Tests.Unit.Application
{
    public class ChangeProductAvailabilityCommandTest
    {
        private readonly ChangeProductAvailabilityCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

        public ChangeProductAvailabilityCommandTest()
        {
            _sut = new ChangeProductAvailabilityCommandHandler(_currentUserServiceMock.Object,
                                                               _productRepositoryMock.Object,
                                                               _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task ChangeProductAvailability_ChangesAvailability()
        {
            var shop = ShopFactory.GetShop();

            var productList = new ProductListMock(shop);

            var command = new ChangeProductAvailabilityCommand()
            {
                Id = productList.Products[0].Id,
            };

            _currentUserServiceMock.Setup(s => s.UserId).Returns(shop.Id);

            _productRepositoryMock.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            Assert.True(productList.Products[0].IsAvailable);

            await _sut.Handle(command, CancellationToken.None);

            _unitOfWorkMock.Verify(x => x.CommitAndDispatchEventsAsync(), Times.Once);

            Assert.False(productList.Products[0].IsAvailable);
        }

        [Fact]
        public async Task ChangeProductAvailability_ThrowsNotFoundIfIdIsInvalid()
        {
            var shop = ShopFactory.GetShop();

            var productList = new ProductListMock(shop);

            var command = new ChangeProductAvailabilityCommand()
            {
                Id = Guid.NewGuid(),
            };

            _currentUserServiceMock.Setup(s => s.UserId).Returns(shop.Id);

            var result = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));

            _unitOfWorkMock.Verify(x => x.CommitAndDispatchEventsAsync(), Times.Never);
        }
    }
}
