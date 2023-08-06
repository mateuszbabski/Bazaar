using Modules.Baskets.Domain.Exceptions;
using Modules.Baskets.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Modules.Baskets.Domain.Entities
{
    public class BasketItem : Entity
    {
        // TODO: Add basket item weight
        public BasketItemId Id { get; private set; }
        public BasketProductId ProductId { get; private set; }
        public BasketShopId ShopId { get; private set; }
        public BasketId BasketId { get; private set; }
        public int Quantity { get; private set; } = 1;
        public MoneyValue Price { get; private set; }
        public MoneyValue BaseCurrencyPrice { get; private set; }
        public Weight BasketItemWeight { get; private set; }
        [JsonIgnore]
        public virtual Basket Basket { get; set; }

        private BasketItem() { }

        private BasketItem(Guid productId,
                           Guid shopId,
                           BasketId basketId,
                           int quantity,
                           string currency,
                           MoneyValue baseProductPrice,
                           decimal productWeight,
                           decimal convertedPrice)
        {
            Id = new BasketItemId(Guid.NewGuid());
            ProductId = productId;
            BasketId = basketId;
            ShopId = shopId;
            Quantity = quantity;
            BaseCurrencyPrice = CountBasketItemPrice(quantity, baseProductPrice.Amount, baseProductPrice.Currency);
            Price = CountBasketItemPrice(quantity, convertedPrice, currency);
            BasketItemWeight = CalculateWeight(quantity, productWeight);
        }

        internal static BasketItem CreateBasketItemFromProduct(Guid productId,
                                                               Guid shopId,
                                                               BasketId basketId,
                                                               int quantity,
                                                               string currency,
                                                               MoneyValue baseProductPrice,
                                                               decimal productWeight,
                                                               decimal convertedPrice)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }

            return new BasketItem(productId, shopId, basketId, quantity, currency, baseProductPrice, productWeight, convertedPrice);
        }

        private static MoneyValue CountBasketItemPrice(int quantity, decimal price, string currency)
        {
            return MoneyValue.Of(quantity * price, currency);
        }

        private static Weight CalculateWeight(int quantity, decimal productWeight)
        {
            var weight = quantity * productWeight;
            return new Weight(weight);
        }

        internal void ChangeBasketItemQuantity(int quantity, decimal price, string currency)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }

            this.Price = CountBasketItemPrice(quantity, price, currency);
            this.BaseCurrencyPrice = CountBasketItemPrice(quantity, this.BaseCurrencyPrice.Amount / this.Quantity, this.BaseCurrencyPrice.Currency);
            this.BasketItemWeight = CalculateWeight(quantity, this.BasketItemWeight / this.Quantity);
            this.Quantity = quantity;
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
