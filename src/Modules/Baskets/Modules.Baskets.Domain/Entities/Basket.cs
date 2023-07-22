using Shared.Domain.ValueObjects;
using Shared.Domain;
using Modules.Baskets.Domain.Events;
using Modules.Baskets.Domain.ValueObjects;
using Modules.Baskets.Domain.Exceptions;

namespace Modules.Baskets.Domain.Entities
{
    public class Basket : Entity, IAggregateRoot
    {
        public BasketId Id { get; private set; }
        public BasketCustomerId CustomerId { get; private set; }
        public List<BasketItem> Items { get; private set; }
        public MoneyValue TotalPrice { get; private set; }

        private Basket() { }

        private Basket(BasketCustomerId customerId, string currency)
        {
            Id = new BasketId(Guid.NewGuid());
            CustomerId = customerId;
            Items = new List<BasketItem>();
            TotalPrice = new MoneyValue(0, currency);
        }

        public static Basket CreateBasket(BasketCustomerId customerId, string currency)
        {
            var basket = new Basket(customerId, currency);
            basket.AddDomainEvent(new BasketCreatedDomainEvent(basket));

            return basket;
        }

        private MoneyValue CountTotalPrice(List<BasketItem> items)
        {
            decimal allProductsPrice = items.Sum(x => x.Price.Amount);

            return new MoneyValue(allProductsPrice, TotalPrice.Currency);
        }

        internal MoneyValue GetPrice()
        {
            return TotalPrice;
        }

        public void ChangeBasketCurrency(decimal conversionRate, string currency)
        {
            foreach (var item in Items)
            {
                item.ChangeCurrency(conversionRate, currency);
            }

            decimal convertedProductsPrice = Items.Sum(x => x.Price.Amount);

            this.TotalPrice = MoneyValue.Of(convertedProductsPrice, currency);

            AddDomainEvent(new BasketCurrencyChangedDomainEvent(this));
        }

        public void AddProductToBasket(Guid productId,
                                       Guid shopId,
                                       int quantity,
                                       MoneyValue baseProductPrice,
                                       decimal convertedPrice)
        {
            var basketItem = Items.FirstOrDefault(x => x.ProductId.Value == productId);

            if (basketItem == null)
            {
                var newBasketItem = BasketItem.CreateBasketItemFromProduct(productId,
                                                                           shopId,
                                                                           this.Id,
                                                                           quantity,
                                                                           this.TotalPrice.Currency,
                                                                           baseProductPrice,
                                                                           convertedPrice);
                Items.Add(newBasketItem);

                this.AddDomainEvent(new ProductAddedToBasketDomainEvent(this, newBasketItem));
            }
            else
            {
                basketItem.ChangeBasketItemQuantity(quantity, convertedPrice, this.TotalPrice.Currency);
                this.AddDomainEvent(new ProductQuantityChangedDomainEvent(this, basketItem));
            }

            this.TotalPrice = CountTotalPrice(this.Items);
        }

        public void RemoveItemFromBasket(BasketItemId basketItemId)
        {
            var item = Items.FirstOrDefault(x => x.Id == basketItemId);

            Items.Remove(item);

            this.AddDomainEvent(new ProductRemovedFromBasketDomainEvent(this, item));

            this.TotalPrice = CountTotalPrice(this.Items);
        }

        public void ChangeBasketItemQuantity(BasketItemId basketItemId,
                                             int quantity)
        {
            var basketItem = Items.FirstOrDefault(x => x.Id == basketItemId) 
                ?? throw new BasketItemNotFoundException();

            if (quantity == basketItem.Quantity)
            {
                return;
            }

            basketItem.ChangeBasketItemQuantity(quantity, (basketItem.Price.Amount / basketItem.Quantity), basketItem.Price.Currency);

            this.AddDomainEvent(new ProductQuantityChangedDomainEvent(this, basketItem));

            this.TotalPrice = CountTotalPrice(this.Items);
        }
    }
}
