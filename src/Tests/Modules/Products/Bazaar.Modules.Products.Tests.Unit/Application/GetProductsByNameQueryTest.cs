using Modules.Products.Application.Dtos;
using Modules.Products.Application.Queries.GetProductsByName;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Repositories;
using Moq;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Bazaar.Modules.Products.Tests.Unit.Application
{
    public class GetProductsByNameQueryTest
    {
        private readonly GetProductsByNameQueryHandler _sut;
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<IQueryProcessor<Product>> _queryProcessorMock = new();
        public GetProductsByNameQueryTest()
        {
            _sut = new GetProductsByNameQueryHandler(_productRepositoryMock.Object, _queryProcessorMock.Object);
        }

        [Fact]
        public async Task GetProductsByName_ReturnsProductList_IfProductNameContainsNameInput()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);

            var productList = productListMock.Products;

            var query = new GetProductsByNameQuery()
            {
                ProductName = "productName"
            };

            var filteredShopList = productList.Where(x => query.ProductName == null
                                                            || x.ProductName.Value.ToLower().Contains(query.ProductName.ToLower()));

            _productRepositoryMock.Setup(x => x.GetProductsByName(query.ProductName))
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
        public async Task GetProductsByName_ReturnsProductList_IfPartOfProductNameContainsNameInput()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);

            var productList = productListMock.Products;

            var query = new GetProductsByNameQuery()
            {
                ProductName = "name"
            };

            var filteredShopList = productList.Where(x => query.ProductName == null
                                                            || x.ProductName.Value.ToLower().Contains(query.ProductName.ToLower()));

            _productRepositoryMock.Setup(x => x.GetProductsByName(query.ProductName))
                               .ReturnsAsync(filteredShopList);

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
        public async Task GetProductsByName_ReturnsProductList_IfNameInputIsEmpty()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);

            var productList = productListMock.Products;

            var query = new GetProductsByNameQuery()
            {
                ProductName = ""
            };

            var filteredShopList = productList.Where(x => query.ProductName == null
                                                            || x.ProductName.Value.ToLower().Contains(query.ProductName.ToLower()));

            _productRepositoryMock.Setup(x => x.GetProductsByName(query.ProductName))
                               .ReturnsAsync(filteredShopList);

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
        public async Task GetProductsByName_ThrowsNotFoundException_IfProductNameDoesntContainNameInput()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);

            var productList = productListMock.Products;

            var query = new GetProductsByNameQuery()
            {
                ProductName = "shop"
            };

            _productRepositoryMock.Setup(x => x.GetProductsByName(query.ProductName))
                               .ThrowsAsync(new NotFoundException("Products not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(query, CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Products not found.", result.Message);
        }
    }
}
