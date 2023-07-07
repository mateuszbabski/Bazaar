using Modules.Products.Application.Dtos;
using Modules.Products.Application.Queries.GetProductsByCategory;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Repositories;
using Modules.Shops.Domain.Entities;
using Modules.Shops.Domain.Repositories;
using Moq;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazaar.Modules.Products.Tests.Unit.Application
{
    public class GetProductsByCategoryQueryTest
    {
        private readonly GetProductsByCategoryQueryHandler _sut;
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<IQueryProcessor<Product>> _queryProcessorMock = new();
        public GetProductsByCategoryQueryTest()
        {
            _sut = new GetProductsByCategoryQueryHandler(_productRepositoryMock.Object, _queryProcessorMock.Object);
        }

        [Fact]
        public async Task GetProductsByCategory_ReturnsProductList_IfProductCategoryContainsNameInput()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);

            var productList = productListMock.Products;

            var query = new GetProductsByCategoryQuery()
            {
                CategoryName = "Food"
            };

            var filteredShopList = productList.Where(x => query.CategoryName == null
                                                            || x.ProductCategory.CategoryName.ToLower().Contains(query.CategoryName.ToLower()));

            _productRepositoryMock.Setup(x => x.GetProductsByCategory(query.CategoryName))
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
        public async Task GetProductsByCategory_ReturnsProductList_IfPartOfProductCategoryContainsNameInput()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);

            var productList = productListMock.Products;

            var query = new GetProductsByCategoryQuery()
            {
                CategoryName = "Fo"
            };

            var filteredShopList = productList.Where(x => query.CategoryName == null
                                                            || x.ProductCategory.CategoryName.ToLower().Contains(query.CategoryName.ToLower()));

            _productRepositoryMock.Setup(x => x.GetProductsByCategory(query.CategoryName))
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
        public async Task GetProductsByCategory_ReturnsProductList_IfNameInputIsEmpty()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);

            var productList = productListMock.Products;

            var query = new GetProductsByCategoryQuery()
            {
                CategoryName = ""
            };

            var filteredShopList = productList.Where(x => query.CategoryName == null
                                                            || x.ProductCategory.CategoryName.ToLower().Contains(query.CategoryName.ToLower()));

            _productRepositoryMock.Setup(x => x.GetProductsByCategory(query.CategoryName))
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
        public async Task GetProductsByCategory_ThrowsNotFoundException_IfProductCategoryDoesntContainNameInput()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);

            var productList = productListMock.Products;

            var query = new GetProductsByCategoryQuery()
            {
                CategoryName = "shop"
            };

            _productRepositoryMock.Setup(x => x.GetProductsByCategory(query.CategoryName))
                               .ThrowsAsync(new NotFoundException("Shops not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(query, CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Shops not found.", result.Message);
        }
    }
}
