using Modules.Shops.Domain.Entities;
using Shared.Domain.ValueObjects;

namespace Modules.Shops.Application.Dtos
{
    public record ShopDto
    {
        public Guid ShopId { get; init; }
        public string ShopName { get; init; }
        public Address ShopAddress { get; init; }

        internal static IEnumerable<ShopDto> CreateDtoFromObject(List<Shop> shops)
        {
            var shopList = new List<ShopDto>();

            foreach(var shop in shops)
            {
                var shopDto = new ShopDto()
                {
                    ShopId = shop.Id,
                    ShopName = shop.ShopName,
                    ShopAddress = shop.ShopAddress
                };

                shopList.Add(shopDto);
            }

            return shopList;
        }
    }
}
