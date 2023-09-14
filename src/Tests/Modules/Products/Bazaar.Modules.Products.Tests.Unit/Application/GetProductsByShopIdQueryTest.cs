using Modules.Products.Application.Dtos;
using Modules.Products.Application.Queries.GetProductsByShopId;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Repositories;
using Moq;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Bazaar.Modules.Products.Tests.Unit.Application
{
    public class GetProductsByShopIdQueryTest
    {
        private readonly GetProductsByShopIdQueryHandler _sut;
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<IQueryProcessor<Product>> _queryProcessorMock = new();
        public GetProductsByShopIdQueryTest()
        {
            _sut = new GetProductsByShopIdQueryHandler(_productRepositoryMock.Object, _queryProcessorMock.Object);
        }

        [Fact]
        public async Task GetProductsByShopId_ReturnsProductList_IfProductNameContainsNameInput()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);

            var productList = productListMock.Products;

            var query = new GetProductsByShopIdQuery()
            {
                ShopId = shop.Id,
            };

            var filteredShopList = productList.Where(x => x.ShopId == query.ShopId);

            _productRepositoryMock.Setup(x => x.GetProductsByShopId(query.ShopId))
                               .ReturnsAsync(filteredShopList)
                               .Verifiable();

            _queryProcessorMock.Setup(x => x.SortQuery(filteredShopList.AsQueryable(), It.IsAny<string>(), It.IsAny<string>()))
                               .Returns(filteredShopList.AsQueryable());
            _queryProcessorMock.Setup(x => x.PageQuery(filteredShopList.AsEnumerable(), It.IsAny<int>(), It.IsAny<int>()))
                               .Returns(filteredShopList.ToList());

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PagedList<ProductDto>>(result);
            Assert.Equal(2, result.Items.Count());

            _productRepositoryMock.Verify();
        }

        [Fact]
        public async Task GetProductsByShopId_ThrowsNotFoundException_IfShopIdIsInvalid()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);

            var productList = productListMock.Products;

            var query = new GetProductsByShopIdQuery()
            {
                ShopId = Guid.NewGuid()
            };

            _productRepositoryMock.Setup(x => x.GetProductsByShopId(query.ShopId))
                               .ThrowsAsync(new NotFoundException("Products not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(query, CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Products not found.", result.Message);
        }
    }
}
