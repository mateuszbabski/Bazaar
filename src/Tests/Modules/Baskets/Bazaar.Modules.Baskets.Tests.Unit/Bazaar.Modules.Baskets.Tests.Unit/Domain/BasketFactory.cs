using Modules.Baskets.Domain.Entities;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.ValueObjects;
using Shared.Domain.ValueObjects;

namespace Bazaar.Modules.Baskets.Tests.Unit.Domain
{
    public static class BasketFactory
    {
        public static Basket GetBasket()
        {
            var basket = Basket.CreateBasket(Guid.NewGuid(), "USD");

            return basket;
        }

        public static Basket GetBasketWithItems()
        {
            var basket = Basket.CreateBasket(Guid.NewGuid(), "USD");
            var product = GetProduct();

            basket.AddProductToBasket(product.Id, product.ShopId, 1, product.Price, product.Price.Amount);

            return basket;
        }

        public static Product GetProduct()
        {
            var product = Product.CreateProduct("Name",
                                                "Description",
                                                ProductCategory.Create("Food"),
                                                1,
                                                MoneyValue.Of(10, "USD"),
                                                "Piece",
                                                Guid.NewGuid());

            return product;
        }
    }
}
