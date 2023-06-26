using Bazaar.Modules.Shops.Tests.Unit.Domain;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Application.Queries.GetShops;
using Modules.Shops.Application.Queries.GetShopsByName;
using Modules.Shops.Domain.Entities;
using Modules.Shops.Domain.Repositories;
using Moq;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;
using System.Xml.Linq;

namespace Bazaar.Modules.Shops.Tests.Unit.Application
{
    public class GetShopsByNameQueryTest
    {
        private readonly GetShopsByNameQueryHandler _sut;
        private readonly Mock<IShopRepository> _shopRepositoryMock = new();

        public GetShopsByNameQueryTest()
        {
            _sut = new GetShopsByNameQueryHandler(_shopRepositoryMock.Object);
        }

        [Fact]
        public async Task GetShopsByName_ReturnsShopList_IfShopNameContainsNameInput()
        {
            var shopList = ShopFactory.GetShopsList();

            var query = new GetShopsByNameQuery()
            {
                ShopName = "Food shop"
            };

            _shopRepositoryMock.Setup(x => x.GetShopsByName(query.ShopName))
                               .ReturnsAsync(shopList.Where(x => query.ShopName == null
                                                            || x.ShopName.Value.ToLower().Contains(query.ShopName.ToLower())));

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ShopDto>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetShopsByName_ReturnsShopList_IfPartOfShopNameContainsNameInput()
        {
            var shopList = ShopFactory.GetShopsList();

            var query = new GetShopsByNameQuery()
            {
                ShopName = "shop"
            };

            _shopRepositoryMock.Setup(x => x.GetShopsByName(query.ShopName))
                               .ReturnsAsync(shopList.Where(x => query.ShopName == null
                                                            || x.ShopName.Value.ToLower().Contains(query.ShopName.ToLower())));

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ShopDto>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetShopsByName_ReturnsShopList_IfNameInputIsEmpty()
        {
            var shopList = ShopFactory.GetShopsList();

            var query = new GetShopsByNameQuery()
            {
                ShopName = ""
            };

            _shopRepositoryMock.Setup(x => x.GetShopsByName(query.ShopName))
                               .ReturnsAsync(shopList.Where(x => query.ShopName == null
                                                            || x.ShopName.Value.ToLower().Contains(query.ShopName.ToLower())));

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ShopDto>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetShopsByName_ThrowsNotFoundException_IfShopNameDoesntContainNameInput()
        {
            var shopList = ShopFactory.GetShopsList();

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
