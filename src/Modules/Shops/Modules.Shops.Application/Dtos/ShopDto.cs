using Modules.Shops.Domain.Entities;

namespace Modules.Shops.Application.Dtos
{
    public record ShopDto
    {
        public Guid ShopId { get; init; }
        public string ShopName { get; init; }

        internal static IEnumerable<ShopDto> CreateDtoFromObject(IEnumerable<Shop> shops)
        {
            var shopList = new List<ShopDto>();

            foreach(var shop in shops)
            {
                var shopDto = new ShopDto()
                {
                    ShopId = shop.Id,
                    ShopName = shop.ShopName
                };

                shopList.Add(shopDto);
            }

            return shopList;
        }
    }
}
