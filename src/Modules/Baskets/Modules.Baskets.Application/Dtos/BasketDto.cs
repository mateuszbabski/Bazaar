using Modules.Baskets.Domain.Entities;
using Shared.Domain.ValueObjects;

namespace Modules.Baskets.Application.Dtos
{
    public class BasketDto
    {
        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public List<BasketItemDto> Items { get; init; }
        public MoneyValue TotalPrice { get; init; }

        public static BasketDto CreateBasketDtoFromObject(Basket basket)
        {
            var basketItemsList = new List<BasketItemDto>();

            foreach (var item in basket.Items)
            {
                var basketItemDto = BasketItemDto.CreateBasketItemDtoFromObject(item);

                basketItemsList.Add(basketItemDto);
            }

            return new BasketDto()
            {
                Id = basket.Id,
                CustomerId = basket.CustomerId,
                TotalPrice = basket.TotalPrice,
                Items = basketItemsList
            };
        }
    }
}
