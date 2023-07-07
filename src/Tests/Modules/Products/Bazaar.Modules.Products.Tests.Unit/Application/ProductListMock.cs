using Modules.Products.Domain.Entities;
using Modules.Products.Domain.ValueObjects;
using Modules.Shops.Domain.Entities;
using Shared.Domain.ValueObjects;

namespace Bazaar.Modules.Products.Tests.Unit.Application
{
    public class ProductListMock
    {
        private readonly List<Product> _productList;
        public List<Product> Products
        {
            get { return _productList; }
        }

        public ProductListMock(Shop shop)
        {
            var product = Product.CreateProduct("productName",
                                                "productDescription",
                                                ProductCategory.Create("Food"),
                                                1,
                                                MoneyValue.Of(10, "USD"),
                                                "pieces",
                                                new ProductShopId(shop.Id));

            var product2 = Product.CreateProduct("productName2",
                                                 "productDescription2",
                                                 ProductCategory.Create("Food"),
                                                 1,
                                                 MoneyValue.Of(20, "USD"),
                                                 "pieces",
                                                 new ProductShopId(shop.Id));

            _productList = new List<Product>
            {
                product,
                product2
            };
        }        
    }
}
