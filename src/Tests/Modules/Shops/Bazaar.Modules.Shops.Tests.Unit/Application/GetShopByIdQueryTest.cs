using Bazaar.Modules.Shops.Tests.Unit.Domain;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Application.Queries.GetShopById;
using Modules.Shops.Domain.Repositories;
using Moq;
using Shared.Application.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bazaar.Modules.Shops.Tests.Unit.Application
{
    public class GetShopByIdQueryTest
    {
        private readonly GetShopByIdQueryHandler _sut;
        private readonly Mock<IShopRepository> _shopRepositoryMock = new(); 
        public GetShopByIdQueryTest()
        {
            _sut = new GetShopByIdQueryHandler(_shopRepositoryMock.Object);
        }

        [Fact]
        public async Task GetShopById_ValidId_ReturnsShopDetailsDto()
        {
            var shop = ShopFactory.GetShop();

            var query = new GetShopByIdQuery()
            {
                Id = shop.Id
            };

            _shopRepositoryMock.Setup(x => x.GetShopById(query.Id)).ReturnsAsync(shop);

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.IsType<ShopDetailsDto>(result);
            Assert.True(shop.Id.Value == result.ShopId);
        }

        [Fact]
        public async Task GetShopById_InvalidId_ThrowsNotFoundException()
        {
            var shop = ShopFactory.GetShop();

            var query = new GetShopByIdQuery()
            {
                Id = Guid.NewGuid()
            };

            _shopRepositoryMock.Setup(x => x.GetShopById(query.Id)).ThrowsAsync(new NotFoundException("Shop not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(query, CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Shop not found.", result.Message);
        }
    }
}
