using Bazaar.Modules.Shops.Tests.Unit.Domain;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Application.Queries.GetShopsByName;
using Modules.Shops.Domain.Entities;
using Modules.Shops.Domain.Repositories;
using Moq;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Bazaar.Modules.Shops.Tests.Unit.Application
{
    public class GetShopsByNameQueryTest
    {
        private readonly GetShopsByNameQueryHandler _sut;
        private readonly Mock<IShopRepository> _shopRepositoryMock = new();
        private readonly Mock<IQueryProcessor<Shop>> _queryProcessorMock = new();

        public GetShopsByNameQueryTest()
        {
            _sut = new GetShopsByNameQueryHandler(_shopRepositoryMock.Object, _queryProcessorMock.Object);
        }

        [Fact]
        public async Task GetShopsByName_ReturnsShopList_IfShopNameContainsNameInput()
        {
            var shopListMock = ShopFactory.GetShopsList();

            var query = new GetShopsByNameQuery()
            {
                ShopName = "Food shop"
            };

            var filteredShopList = shopListMock.Where(x => query.ShopName == null
                                                            || x.ShopName.Value.ToLower().Contains(query.ShopName.ToLower()));

            _shopRepositoryMock.Setup(x => x.GetShopsByName(query.ShopName))
                               .ReturnsAsync(filteredShopList)
                               .Verifiable();

            _queryProcessorMock.Setup(x => x.SortQuery(filteredShopList.AsQueryable(), It.IsAny<string>(), It.IsAny<string>()))
                               .Returns(filteredShopList.AsQueryable());
            _queryProcessorMock.Setup(x => x.PageQuery(filteredShopList.AsEnumerable(), It.IsAny<int>(), It.IsAny<int>()))
                               .Returns(filteredShopList.ToList());

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PagedList<ShopDto>>(result);
            Assert.Single(result.Items);

            _shopRepositoryMock.Verify();
        }

        [Fact]
        public async Task GetShopsByName_ReturnsShopList_IfPartOfShopNameContainsNameInput()
        {
            var shopListMock = ShopFactory.GetShopsList();

            var query = new GetShopsByNameQuery()
            {
                ShopName = "shop"
            };

            var filteredShopList = shopListMock.Where(x => query.ShopName == null
                                                            || x.ShopName.Value.ToLower().Contains(query.ShopName.ToLower()));

            _shopRepositoryMock.Setup(x => x.GetShopsByName(query.ShopName))
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
        public async Task GetShopsByName_ReturnsShopList_IfNameInputIsEmpty()
        {
            var shopListMock = ShopFactory.GetShopsList();

            var query = new GetShopsByNameQuery()
            {
                ShopName = ""
            };

            var filteredShopList = shopListMock.Where(x => query.ShopName == null
                                                            || x.ShopName.Value.ToLower().Contains(query.ShopName.ToLower()));

            _shopRepositoryMock.Setup(x => x.GetShopsByName(query.ShopName))
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
        public async Task GetShopsByName_ThrowsNotFoundException_IfShopNameDoesntContainNameInput()
        {
            var shopListMock = ShopFactory.GetShopsList();

            var query = new GetShopsByNameQuery()
            {
                ShopName = "shop"
            };
            
            _shopRepositoryMock.Setup(x => x.GetShopsByName(query.ShopName))
                               .ThrowsAsync(new NotFoundException("Shops not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(query, CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Shops not found.", result.Message);
        }
    }
}
