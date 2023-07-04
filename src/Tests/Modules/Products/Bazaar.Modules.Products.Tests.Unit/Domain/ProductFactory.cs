using Modules.Products.Domain.Entities;
using Modules.Products.Domain.ValueObjects;
using Shared.Domain.ValueObjects;

namespace Bazaar.Modules.Products.Tests.Unit.Domain
{
    public static class ProductFactory
    {
        public static Product GetProduct()
        {
            var product = Product.CreateProduct("productName",
                                                "productDescription",
                                                ProductCategory.Create("Food"),
                                                1,
                                                MoneyValue.Of(10, "USD"),
                                                "piece",
                                                Guid.NewGuid());

            return product;
        }        
    }
}
