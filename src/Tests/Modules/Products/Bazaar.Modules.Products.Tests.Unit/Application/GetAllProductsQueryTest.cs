using Modules.Products.Application.Dtos;
using Modules.Products.Application.Queries.GetAllProducts;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Repositories;
using Modules.Shops.Domain.Entities;
using Modules.Shops.Domain.Repositories;
using Moq;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Bazaar.Modules.Products.Tests.Unit.Application
{
    public class GetAllProductsQueryTest
    {
        private readonly GetAllProductsQueryHandler _sut;
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<IQueryProcessor<Product>> _queryProcessorMock = new();
        public GetAllProductsQueryTest()
        {
            _sut = new GetAllProductsQueryHandler(_productRepositoryMock.Object, _queryProcessorMock.Object);
        }

        [Fact]
        public async Task GetAllProducts_ShopsInList_ReturnsShopList()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);  
            
            var productList = productListMock.Products;

            _productRepositoryMock.Setup(x => x.GetAllProducts()).ReturnsAsync(productList);

            _queryProcessorMock.Setup(x => x.SortQuery(productList.AsQueryable(), It.IsAny<string>(), It.IsAny<string>()))
                               .Returns(productList.AsQueryable());
            _queryProcessorMock.Setup(x => x.PageQuery(productList.AsEnumerable(), It.IsAny<int>(), It.IsAny<int>()))
                               .Returns(productList);

            var result = await _sut.Handle(new GetAllProductsQuery(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PagedList<ProductDto>>(result);
            Assert.Equal(result.Items.First().Id, productList.First().Id.Value);
        }

        [Fact]
        public async Task GetAllProducts_NoProductsInDb_ThrowsNotFoundException()
        {
            _productRepositoryMock.Setup(x => x.GetAllProducts()).ThrowsAsync(new NotFoundException("Products not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(new GetAllProductsQuery(), CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Products not found.", result.Message);
        }
    }
}
