using Bazaar.Modules.Shops.Tests.Unit.Domain;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Application.Queries.GetShopsByLocalization;
using Modules.Shops.Domain.Entities;
using Modules.Shops.Domain.Repositories;
using Moq;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Bazaar.Modules.Shops.Tests.Unit.Application
{
    public class GetShopsByLocalizationQueryTest
    {
        private readonly GetShopsByLocalizationQueryHandler _sut;
        private readonly Mock<IShopRepository> _shopRepositoryMock = new();
        private readonly Mock<IQueryProcessor<Shop>> _queryProcessorMock = new();

        public GetShopsByLocalizationQueryTest()
        {
            _sut = new GetShopsByLocalizationQueryHandler(_shopRepositoryMock.Object, _queryProcessorMock.Object);
        }

        [Fact]
        public async Task GetShopsByName_ReturnsShopList_IfAddressContainsAddressInput()
        {
            var shopListMock = ShopFactory.GetShopsList();

            var query = new GetShopsByLocalizationQuery()
            {
                Country = "Poland",
                City = "Warsaw"
            };

            var filteredShopList = shopListMock.Where(x => query.Country == null
                                                            || x.ShopAddress.Country.ToLower().Contains(query.Country.ToLower()))
                                               .Where(x => query.City == null
                                                            || x.ShopAddress.City.ToLower().Contains(query.City.ToLower()));
            _shopRepositoryMock.Setup(x => x.GetShopsByLocalization(query.Country, query.City))
                               .ReturnsAsync(filteredShopList);

            _queryProcessorMock.Setup(x => x.SortQuery(filteredShopList.AsQueryable(), It.IsAny<string>(), It.IsAny<string>()))
                               .Returns(filteredShopList.AsQueryable());
            _queryProcessorMock.Setup(x => x.PageQuery(filteredShopList.AsEnumerable(), It.IsAny<int>(), It.IsAny<int>()))
                               .Returns(filteredShopList.ToList());

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PagedList<ShopDto>>(result);
            Assert.Single(result.Items);
        }

        [Fact]
        public async Task GetShopsByLocalization_ReturnsShopList_IfAddressContainsCountryInput()
        {
            var shopListMock = ShopFactory.GetShopsList();

            var query = new GetShopsByLocalizationQuery()
            {
                Country = "Poland",
                City = ""
            };

            var filteredShopList = shopListMock.Where(x => query.Country == null
                                                            || x.ShopAddress.Country.ToLower().Contains(query.Country.ToLower()))
                                               .Where(x => query.City == null
                                                            || x.ShopAddress.City.ToLower().Contains(query.City.ToLower()));
            _shopRepositoryMock.Setup(x => x.GetShopsByLocalization(query.Country, query.City))
                               .ReturnsAsync(filteredShopList);

            _queryProcessorMock.Setup(x => x.SortQuery(filteredShopList.AsQueryable(), It.IsAny<string>(), It.IsAny<string>()))
                               .Returns(filteredShopList.AsQueryable());
            _queryProcessorMock.Setup(x => x.PageQuery(filteredShopList.AsEnumerable(), It.IsAny<int>(), It.IsAny<int>()))
                               .Returns(filteredShopList.ToList());

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PagedList<ShopDto>>(result);
            Assert.Equal(2, result.Items.Count());
        }

        [Fact]
        public async Task GetShopsByLocalization_ReturnsShopList_IfAddressContainsCityInput()
        {
            var shopListMock = ShopFactory.GetShopsList();

            var query = new GetShopsByLocalizationQuery()
            {
                Country = "",
                City = "Warsaw"
            };

            var filteredShopList = shopListMock.Where(x => query.Country == null
                                                             || x.ShopAddress.Country.ToLower().Contains(query.Country.ToLower()))
                                               .Where(x => query.City == null
                                                             || x.ShopAddress.City.ToLower().Contains(query.City.ToLower()));
            _shopRepositoryMock.Setup(x => x.GetShopsByLocalization(query.Country, query.City))
                               .ReturnsAsync(filteredShopList);

            _queryProcessorMock.Setup(x => x.SortQuery(filteredShopList.AsQueryable(), It.IsAny<string>(), It.IsAny<string>()))
                               .Returns(filteredShopList.AsQueryable());
            _queryProcessorMock.Setup(x => x.PageQuery(filteredShopList.AsEnumerable(), It.IsAny<int>(), It.IsAny<int>()))
                               .Returns(filteredShopList.ToList());

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
            var shopListMock = ShopFactory.GetShopsList();

            var query = new GetShopsByLocalizationQuery()
            {
                Country = "",
                City = ""
            };

            var filteredShopList = shopListMock.Where(x => query.Country == null
                                                            || x.ShopAddress.Country.ToLower().Contains(query.Country.ToLower()))
                                               .Where(x => query.City == null
                                                            || x.ShopAddress.City.ToLower().Contains(query.City.ToLower()));
            _shopRepositoryMock.Setup(x => x.GetShopsByLocalization(query.Country, query.City))
                               .ReturnsAsync(filteredShopList);

            _queryProcessorMock.Setup(x => x.SortQuery(filteredShopList.AsQueryable(), It.IsAny<string>(), It.IsAny<string>()))
                               .Returns(filteredShopList.AsQueryable());
            _queryProcessorMock.Setup(x => x.PageQuery(filteredShopList.AsEnumerable(), It.IsAny<int>(), It.IsAny<int>()))
                               .Returns(filteredShopList.ToList());

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PagedList<ShopDto>>(result);
            Assert.Equal(2, result.Items.Count());
        }        
    }
}
