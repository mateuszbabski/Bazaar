﻿using Modules.Baskets.Domain.Entities;
using Shared.Domain.Exceptions;

namespace Bazaar.Modules.Baskets.Tests.Unit.Domain
{
    public class BasketDomainTest
    {
        [Fact]
        public void CreateBasket_ReturnsBasket_IfParamsValid()
        {
            var basket = Basket.CreateBasket(Guid.NewGuid(), "USD");

            Assert.NotNull(basket);
            Assert.IsType<Basket>(basket);
        }

        [Fact]
        public void AddProductToBasket_ReturnsIfSuccessfully()
        {
            var product = BasketFactory.GetProduct();
            var basket = BasketFactory.GetBasket();

            basket.AddProductToBasket(product.Id, product.ShopId, 1, product.Price, product.WeightPerUnit, product.Price.Amount);


            var basketItem = basket.Items.FirstOrDefault(x => x.ProductId == product.Id.Value);

            Assert.Equal(1, basketItem?.Quantity);
            Assert.Equal(product.Id.Value, basketItem?.ProductId);
            Assert.IsType<BasketItem>(basketItem);
        }

        [Fact]
        public void RemoveProductFromBasket_RemovesBasketItemFromBasket()
        {
            var product = BasketFactory.GetProduct();
            var basket = BasketFactory.GetBasket();

            basket.AddProductToBasket(product.Id, product.ShopId, 1, product.Price, product.WeightPerUnit, product.Price.Amount);

            var basketItem = basket.Items.FirstOrDefault(x => x.ProductId == product.Id.Value);

            Assert.Equal(product.Id.Value, basketItem?.ProductId);
            Assert.IsType<BasketItem>(basketItem);

            basket.RemoveItemFromBasket(basketItem.Id);
            var item = basket.Items.FirstOrDefault(x => x.Id == basketItem.Id);

            Assert.IsNotType<BasketItem>(item);
            Assert.Null(item);
        }

        [Fact]
        public void ChangeProductQuantityInBasket_UpdatessBasketItemQuantityInBasket()
        {
            var product = BasketFactory.GetProduct();
            var basket = BasketFactory.GetBasket();

            basket.AddProductToBasket(product.Id, product.ShopId, 1, product.Price, product.WeightPerUnit, product.Price.Amount);

            var basketItem = basket.Items.FirstOrDefault(x => x.ProductId == product.Id.Value);

            Assert.Equal(product.Id.Value, basketItem?.ProductId);
            Assert.Equal(1, basketItem?.Quantity);
            Assert.IsType<BasketItem>(basketItem);

            basket.ChangeBasketItemQuantity(basketItem.Id, 2);
            Assert.Equal(2, basketItem?.Quantity);
        }

        [Fact]
        public void ChangeBasketCurrency_ChangesCurrency_IfCurrencyAvailable()
        {
            decimal conversionRate = 4.0M;
            var product = BasketFactory.GetProduct();
            var basket = BasketFactory.GetBasket();

            basket.AddProductToBasket(product.Id, product.ShopId, 1, product.Price, product.WeightPerUnit, product.Price.Amount);

            basket.ChangeBasketCurrency(conversionRate, "PLN");

            Assert.Equal("PLN", basket.TotalPrice.Currency);
        }

        [Fact]
        public void ChangeBasketCurrency_Throws_IfCurrencyNotAcceptedByTheSystem()
        {
            decimal conversionRate = 4.0M;
            var product = BasketFactory.GetProduct();
            var basket = BasketFactory.GetBasket();

            basket.AddProductToBasket(product.Id, product.ShopId, 1, product.Price, product.WeightPerUnit, product.Price.Amount);

            var act = Assert.Throws<InvalidPriceException>(() => basket.ChangeBasketCurrency(conversionRate, "PES"));
            Assert.IsType<InvalidPriceException>(act);
        }
    }
}
