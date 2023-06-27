using Bazaar.Modules.Shops.Tests.Unit.Domain;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Application.Queries.GetShopsByLocalization;
using Modules.Shops.Domain.Repositories;
using Moq;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Bazaar.Modules.Shops.Tests.Unit.Application
{
    public class GetShopsByLocalizationQueryTest
    {
        private readonly GetShopsByLocalizationQueryHandler _sut;
        private readonly Mock<IShopRepository> _shopRepositoryMock = new();

        public GetShopsByLocalizationQueryTest()
        {
            _sut = new GetShopsByLocalizationQueryHandler(_shopRepositoryMock.Object);
        }

        [Fact]
        public async Task GetShopsByName_ReturnsShopList_IfAddressContainsAddressInput()
        {
            var shopList = ShopFactory.GetShopsList();

            var query = new GetShopsByLocalizationQuery()
            {
                Country = "Poland",
                City = "Warsaw"
            };

            _shopRepositoryMock.Setup(x => x.GetShopsByLocalization(query.Country, query.City))
                               .ReturnsAsync(shopList.Where(x => query.Country == null
                                                            || x.ShopAddress.Country.ToLower().Contains(query.Country.ToLower()))
                                                     .Where(x => query.City == null
                                                            || x.ShopAddress.City.ToLower().Contains(query.City.ToLower())));

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PagedList<ShopDto>>(result);
            Assert.Single(result.Items);
        }

        [Fact]
        public async Task GetShopsByLocalization_ReturnsShopList_IfAddressContainsCountryInput()
        {
            var shopList = ShopFactory.GetShopsList();

            var query = new GetShopsByLocalizationQuery()
            {
                Country = "Poland",
                City = ""
            };

            _shopRepositoryMock.Setup(x => x.GetShopsByLocalization(query.Country, query.City))
                               .ReturnsAsync(shopList.Where(x => query.Country == null
                                                            || x.ShopAddress.Country.ToLower().Contains(query.Country.ToLower()))
                                                     .Where(x => query.City == null
                                                            || x.ShopAddress.City.ToLower().Contains(query.City.ToLower())));

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PagedList<ShopDto>>(result);
            Assert.Equal(2, result.Items.Count());
        }

        [Fact]
        public async Task GetShopsByLocalization_ReturnsShopList_IfAddressContainsCityInput()
        {
            var shopList = ShopFactory.GetShopsList();

            var query = new GetShopsByLocalizationQuery()
            {
                Country = "",
                City = "Warsaw"
            };

            _shopRepositoryMock.Setup(x => x.GetShopsByLocalization(query.Country, query.City))
                               .ReturnsAsync(shopList.Where(x => query.Country == null
                                                            || x.ShopAddress.Country.ToLower().Contains(query.Country.ToLower()))
                                                     .Where(x => query.City == null
                                                            || x.ShopAddress.City.ToLower().Contains(query.City.ToLower())));

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PagedList<ShopDto>>(result);
            Assert.Single(result.Items);
        }

        [Fact]
        public async Task GetShopsByLocalization_ThrowsNotFound_IfAddressDoesntContainOneInput()
        {
            var shopList = ShopFactory.GetShopsList();

            var query = new GetShopsByLocalizationQuery()
            {
                Country = "Poland",
                City = "Cracow"
            };

            _shopRepositoryMock.Setup(x => x.GetShopsByLocalization(query.Country, query.City))
                               .ThrowsAsync(new NotFoundException("Shops not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(query, CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Shops not found.", result.Message);
        }

        [Fact]
        public async Task GetShopsByLocalization_ThrowsNotFound_IfAddressDoesntContainCityInput()
        {
            var shopList = ShopFactory.GetShopsList();

            var query = new GetShopsByLocalizationQuery()
            {
                Country = "",
                City = "Cracow"
            };

            _shopRepositoryMock.Setup(x => x.GetShopsByLocalization(query.Country, query.City))
                               .ThrowsAsync(new NotFoundException("Shops not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(query, CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Shops not found.", result.Message);
        }

        [Fact]
        public async Task GetShopsByLocalization_ThrowsNotFound_IfAddressDoesntContainCountryInput()
        {
            var shopList = ShopFactory.GetShopsList();

            var query = new GetShopsByLocalizationQuery()
            {
                Country = "USA",
                City = ""
            };

            _shopRepositoryMock.Setup(x => x.GetShopsByLocalization(query.Country, query.City))
                               .ThrowsAsync(new NotFoundException("Shops not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(query, CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Shops not found.", result.Message);
        }

        [Fact]
        public async Task GetShopsByLocalization_ReturnsShopList_IfInputIsEmpty()
        {
            var shopList = ShopFactory.GetShopsList();

            var query = new GetShopsByLocalizationQuery()
            {
                Country = "",
                City = ""
            };

            _shopRepositoryMock.Setup(x => x.GetShopsByLocalization(query.Country, query.City))
                               .ReturnsAsync(shopList.Where(x => query.Country == null
                                                            || x.ShopAddress.Country.ToLower().Contains(query.Country.ToLower()))
                                                     .Where(x => query.City == null 
                                                            || x.ShopAddress.City.ToLower().Contains(query.City.ToLower())));

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PagedList<ShopDto>>(result);
            Assert.Equal(2, result.Items.Count());
        }        
    }
}
