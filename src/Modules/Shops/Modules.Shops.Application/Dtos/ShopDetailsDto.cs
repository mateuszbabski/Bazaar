using Modules.Shops.Domain.Entities;
using Shared.Domain.ValueObjects;

namespace Modules.Shops.Application.Dtos
{
    public record ShopDetailsDto
    {
        public Guid ShopId { get; init; }
        public string ShopEmail { get; init; }
        public string OwnerName { get; init; }
        public string OwnerLastName { get; init; }
        public string ShopName { get; init; }
        public string TaxNumber { get; init; }  
        public Address ShopAddress { get; init; }
        public string ShopContactNumber { get; init; }

        public static ShopDetailsDto CreateDtoFromObject(Shop shop)
        {
            return new ShopDetailsDto
            {
                ShopId = shop.Id,
                ShopEmail = shop.Email,
                OwnerName = shop.OwnerName,
                OwnerLastName = shop.OwnerLastName,
                ShopName = shop.ShopName,
                TaxNumber = shop.TaxNumber,
                ShopAddress = shop.ShopAddress,
                ShopContactNumber = shop.ContactNumber
            };
        }
    }
}
