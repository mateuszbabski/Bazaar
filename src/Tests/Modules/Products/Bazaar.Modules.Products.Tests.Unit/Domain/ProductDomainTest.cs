using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Exceptions;
using Modules.Products.Domain.ValueObjects;
using Shared.Domain.Exceptions;
using Shared.Domain.ValueObjects;

namespace Bazaar.Modules.Products.Tests.Unit.Domain
{
    public class ProductDomainTest
    {
        [Fact]
        public void CreateProduct_ReturnsProduct_IfAllParamsValid()
        {
            var product = Product.CreateProduct("productName",
                                                "productDescription",
                                                ProductCategory.Create("Food"),
                                                1,
                                                MoneyValue.Of(10, "USD"),
                                                "piece",
                                                Guid.NewGuid()); 
            
            Assert.NotNull(product);
            Assert.IsType<Product>(product);
            Assert.IsType<ProductId>(product.Id);
            Assert.Equal("productName", product.ProductName);
            Assert.Equal(10, product.Price.Amount);
        }

        [Fact]
        public void CreateProduct_ThrowsInvalidName_WhenNameParamIsEmpty()
        {
            var act = Assert.Throws<InvalidProductNameException>(() =>
                          Product.CreateProduct("",
                                                "productDescription",
                                                ProductCategory.Create("Food"),
                                                1,
                                                MoneyValue.Of(10, "USD"),
                                                "piece",
                                                Guid.NewGuid()));

            Assert.IsType<InvalidProductNameException>(act);
            Assert.Equal("Product name cannot be empty.", act.Message);
        }

        [Fact]
        public void CreateProduct_ThrowsInvalidDescription_WhenDescriptionParamIsEmpty()
        {
            var act = Assert.Throws<InvalidProductDescriptionException>(() => 
                          Product.CreateProduct("productName",
                                                "",
                                                ProductCategory.Create("Food"),
                                                1,
                                                MoneyValue.Of(10, "USD"),
                                                "piece",
                                                Guid.NewGuid()));

            Assert.IsType<InvalidProductDescriptionException>(act);
            Assert.Equal("Product description cannot be empty.", act.Message);
        }

        [Fact]
        public void CreateProduct_ThrowsInvalidPriceException_IfPriceIsInvalid()
        {
            var act = Assert.Throws<InvalidPriceException>(() => 
                          Product.CreateProduct("productName",
                                                "productDescription",
                                                ProductCategory.Create("Food"),
                                                1,
                                                MoneyValue.Of(10, "PES"),
                                                "piece",
                                                Guid.NewGuid()));

            Assert.IsType<InvalidPriceException>(act);
            Assert.Equal("Invalid currency.", act.Message);
        }

        [Fact]
        public void CreateProduct_ThrowsInvalidProductCategoryException_IfCategoryIsNotAccepted()
        {
            var act = Assert.Throws<InvalidProductCategoryException>(() => 
                          Product.CreateProduct("productName",
                                                "productDescription",
                                                ProductCategory.Create(""),
                                                1,
                                                MoneyValue.Of(10, "USD"),
                                                "piece",
                                                Guid.NewGuid()));

            Assert.IsType<InvalidProductCategoryException>(act);
            Assert.Equal("Invalid product category.", act.Message);
        }
    }
}
