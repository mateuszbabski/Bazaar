using Modules.Baskets.Domain.Exceptions;
using Modules.Baskets.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Modules.Baskets.Domain.Entities
{
    public class BasketItem : Entity
    {
        public BasketItemId Id { get; private set; }
        public BasketProductId ProductId { get; private set; }
        public BasketShopId ShopId { get; private set; }
        public BasketId BasketId { get; private set; }
        public int Quantity { get; private set; } = 1;
        public MoneyValue Price { get; private set; }
        public MoneyValue BaseCurrencyPrice { get; private set; }
        [JsonIgnore]
        public virtual Basket Basket { get; set; }

        private BasketItem() { }
        
        private BasketItem(Guid productId,
                           Guid shopId,
                           BasketId basketId,
                           int quantity,
                           string currency,
                           MoneyValue baseProductPrice,
                           decimal convertedPrice)
        {
            Id = new BasketItemId(Guid.NewGuid());
            ProductId = productId;
            BasketId = basketId;
            ShopId = shopId;
            Quantity = quantity;
            BaseCurrencyPrice = CountBasketItemPrice(quantity, baseProductPrice.Amount, baseProductPrice.Currency);
            Price = CountBasketItemPrice(quantity, convertedPrice, currency);
        }

        internal static BasketItem CreateBasketItemFromProduct(Guid productId,
                                                               Guid shopId,
                                                               BasketId basketId,
                                                               int quantity,
                                                               string currency,
                                                               MoneyValue baseProductPrice,
                                                               decimal convertedPrice)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }

            return new BasketItem(productId, shopId, basketId, quantity, currency, baseProductPrice, convertedPrice);
        }

        private static MoneyValue CountBasketItemPrice(int quantity, decimal price, string currency)
        {
            return MoneyValue.Of(quantity * price, currency);
        }

        internal void ChangeBasketItemQuantity(int quantity, decimal price, string currency)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }

            this.Quantity = quantity;

            this.Price = CountBasketItemPrice(quantity, price, currency);
        }

        internal void ChangeCurrency(decimal conversionRate, string currency)
        {
            if (this.BaseCurrencyPrice.Currency != currency)
            {
                this.Price = MoneyValue.Of(this.Price.Amount * conversionRate,
                                           currency);
            }
            else
            {
                this.Price = this.BaseCurrencyPrice;
            }
        }
    }
}
