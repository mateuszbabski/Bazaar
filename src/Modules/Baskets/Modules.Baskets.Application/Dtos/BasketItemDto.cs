using Modules.Baskets.Domain.Entities;
using Shared.Domain.ValueObjects;

namespace Modules.Baskets.Application.Dtos
{
    public record BasketItemDto
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public MoneyValue Price { get; init; }
        public Weight Weight { get; init; }

        public static BasketItemDto CreateBasketItemDtoFromObject(BasketItem basketItem)
        {
            return new BasketItemDto()
            {
                Id = basketItem.Id,
                ProductId = basketItem.ProductId,
                Quantity = basketItem.Quantity,
                Price = basketItem.Price,
                Weight = basketItem.BasketItemWeight
            };
        }
    }
}
