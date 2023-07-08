using Modules.Products.Application.Dtos;
using Modules.Products.Application.Queries.GetProductsByName;
using Modules.Products.Application.Queries.GetProductsByPriceRange;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Repositories;
using Moq;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Bazaar.Modules.Products.Tests.Unit.Application
{
    public class GetProductsByPriceRangeQueryTest
    {
        private readonly GetProductsByPriceRangeQueryHandler _sut;
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<IQueryProcessor<Product>> _queryProcessorMock = new();
        public GetProductsByPriceRangeQueryTest()
        {
            _sut = new GetProductsByPriceRangeQueryHandler(_productRepositoryMock.Object, _queryProcessorMock.Object);   
        }

        [Fact]
        public async Task GetProductsByPriceRange_ReturnsProductList_IfProductArePresentInRange()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);

            var productList = productListMock.Products;

            var query = new GetProductsByPriceRangeQuery()
            {
                MinPrice = 5,
                MaxPrice = 30,
            };

            var filteredShopList = productList.Where(x => query.MinPrice == null
                                                        || x.Price.Amount >= query.MinPrice)
                                              .Where(x => query.MaxPrice == null
                                                        || x.Price.Amount <= query.MaxPrice);

            _productRepositoryMock.Setup(x => x.GetProductsByPriceRange(query.MinPrice, query.MaxPrice))
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
        public async Task GetProductsByPriceRange_ReturnsProductList_IfAtLeastOneProductIsInRange()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);

            var productList = productListMock.Products;

            var query = new GetProductsByPriceRangeQuery()
            {
                MinPrice = 10,
                MaxPrice = 15,
            };

            var filteredShopList = productList.Where(x => query.MinPrice == null
                                                        || x.Price.Amount >= query.MinPrice)
                                              .Where(x => query.MaxPrice == null
                                                        || x.Price.Amount <= query.MaxPrice);

            _productRepositoryMock.Setup(x => x.GetProductsByPriceRange(query.MinPrice, query.MaxPrice))
                               .ReturnsAsync(filteredShopList)
                               .Verifiable();

            _queryProcessorMock.Setup(x => x.SortQuery(filteredShopList.AsQueryable(), It.IsAny<string>(), It.IsAny<string>()))
                               .Returns(filteredShopList.AsQueryable());
            _queryProcessorMock.Setup(x => x.PageQuery(filteredShopList.AsEnumerable(), It.IsAny<int>(), It.IsAny<int>()))
                               .Returns(filteredShopList.ToList());

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PagedList<ProductDto>>(result);
            Assert.Single(result.Items);
        }

        [Fact]
        public async Task GetProductsByPriceRange_ReturnsProductList_IfPriceInputIsEmpty()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);

            var productList = productListMock.Products;

            var query = new GetProductsByPriceRangeQuery()
            {
                
            };

            var filteredShopList = productList.Where(x => query.MinPrice == null
                                                        || x.Price.Amount >= query.MinPrice)
                                              .Where(x => query.MaxPrice == null
                                                        || x.Price.Amount <= query.MaxPrice);

            _productRepositoryMock.Setup(x => x.GetProductsByPriceRange(query.MinPrice, query.MaxPrice))
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
        }

        [Fact]
        public async Task GetProductsByPriceRange_ThrowsNotFoundException_IfThereIsNoProductsInPriceRange()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);

            var productList = productListMock.Products;

            var query = new GetProductsByPriceRangeQuery()
            {
                MinPrice = 50
            };

            _productRepositoryMock.Setup(x => x.GetProductsByPriceRange(query.MinPrice, query.MaxPrice))
                               .ThrowsAsync(new NotFoundException("Products not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(query, CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Products not found.", result.Message);
        }
    }
}
