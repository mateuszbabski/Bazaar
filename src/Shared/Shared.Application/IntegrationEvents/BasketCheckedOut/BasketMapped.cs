using Shared.Domain.ValueObjects;

namespace Shared.Application.IntegrationEvents.BasketCheckedOut
{
    public class BasketMapped
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<BasketItemMapped> Items { get; set; }
        public MoneyValue TotalPrice { get; set; }
    }
}
