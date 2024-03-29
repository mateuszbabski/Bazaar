﻿using Bazaar.Modules.Shops.Tests.Unit.Domain;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Application.Queries.GetShops;
using Modules.Shops.Domain.Entities;
using Modules.Shops.Domain.Repositories;
using Moq;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Bazaar.Modules.Shops.Tests.Unit.Application
{
    public class GetShopsQueryTest
    {
        private readonly GetShopsQueryHandler _sut;
        private readonly Mock<IShopRepository> _shopRepositoryMock = new();
        private readonly Mock<IQueryProcessor<Shop>> _queryProcessorMock = new();
        public GetShopsQueryTest()
        {
            _sut = new GetShopsQueryHandler(_shopRepositoryMock.Object, _queryProcessorMock.Object);
        }

        [Fact]
        public async Task GetShops_ShopsInList_ReturnsShopList()
        {
            var shop1 = ShopFactory.GetShop();
            var shop2 = ShopFactory.GetShop();

            var shopListMock = new List<Shop>
            {
                shop1,
                shop2
            };

            _shopRepositoryMock.Setup(x => x.GetAllShops()).ReturnsAsync(shopListMock);

            _queryProcessorMock.Setup(x => x.SortQuery(shopListMock.AsQueryable(), It.IsAny<string>(), It.IsAny<string>()))
                               .Returns(shopListMock.AsQueryable());
            _queryProcessorMock.Setup(x => x.PageQuery(shopListMock.AsEnumerable(), It.IsAny<int>(), It.IsAny<int>()))
                               .Returns(shopListMock);

            var result = await _sut.Handle(new GetShopsQuery(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PagedList<ShopDto>>(result);
            Assert.Equal(result.Items.First().ShopId, shop1.Id.Value);
        }

        [Fact]
        public async Task GetShops_NoShopsInDb_ThrowsNotFoundException()
        {
            _shopRepositoryMock.Setup(x => x.GetAllShops()).ThrowsAsync(new NotFoundException("Shops not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(() 
                => _sut.Handle(new GetShopsQuery(), CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Shops not found.", result.Message);
        }
    }
}
