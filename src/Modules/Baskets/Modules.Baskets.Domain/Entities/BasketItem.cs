using Modules.Baskets.Domain.Exceptions;
using Modules.Baskets.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.ValueObjects;

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

        private BasketItem() { }

        // get product / projection
        private BasketItem(Product product,
                           BasketId basketId,
                           int quantity,
                           string currency,
                           decimal convertedPrice)
        {
            Id = new BasketItemId(Guid.NewGuid());
            ProductId = product.Id;
            BasketId = basketId;
            ShopId = product.ShopId;
            Quantity = quantity;
            BaseCurrencyPrice = CountCartItemPrice(quantity, product.Price.Amount, product.Price.Currency);
            Price = CountCartItemPrice(quantity, convertedPrice, currency);
        }

        internal static BasketItem CreateBasketItemFromProduct(Product product,
                                                               BasketId basketId,
                                                               int quantity,
                                                               string currency,
                                                               decimal convertedPrice)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }

            return new BasketItem(product, basketId, quantity, currency, convertedPrice);
        }

        private static MoneyValue CountCartItemPrice(int quantity, decimal price, string currency)
        {
            return MoneyValue.Of(quantity * price, currency);
        }

        internal void ChangeCartItemQuantity(int quantity, decimal price, string currency)
        {
            if (quantity <= 0)
            {
                throw new InvalidQuantityException();
            }

            this.Quantity = quantity;

            this.Price = CountCartItemPrice(quantity, price, currency);
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
