using Shared.Domain.ValueObjects;

namespace Shared.Application.IntegrationEvents.BasketCheckedOut
{
    public class BasketItemMapped
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ShopId { get; set; }
        public Guid BasketId { get; set; }
        public int Quantity { get; set; }
        public MoneyValue Price { get; set; }
        public MoneyValue BaseCurrencyPrice { get; set; }
    }
}
