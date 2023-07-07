using Modules.Products.Application.Dtos;
using Modules.Products.Application.Queries.GetProductById;
using Modules.Products.Domain.Repositories;
using Moq;
using Shared.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazaar.Modules.Products.Tests.Unit.Application
{
    public class GetProductByIdQueryTest
    {
        private readonly GetProductByIdQueryHandler _sut;
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        public GetProductByIdQueryTest()
        {
            _sut = new GetProductByIdQueryHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task GetProductById_ValidId_ReturnsProductDetailsDto()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);
            var productList = productListMock.Products;

            var query = new GetProductByIdQuery()
            {
                Id = productList.First().Id
            };

            _productRepositoryMock.Setup(x => x.GetById(query.Id)).ReturnsAsync(productList.First());

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.IsType<ProductDetailsDto>(result);
            Assert.True(productList.First().Id.Value == result.Id);
        }

        [Fact]
        public async Task GetProductById_InvalidId_ThrowsNotFoundException()
        {
            var shop = ShopFactory.GetShop();
            var productListMock = new ProductListMock(shop);
            var productList = productListMock.Products;

            var query = new GetProductByIdQuery()
            {
                Id = Guid.NewGuid()
            };

            _productRepositoryMock.Setup(x => x.GetById(query.Id)).ThrowsAsync(new NotFoundException("Product not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(query, CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Product not found.", result.Message);
        }
    }
}
