using Modules.Shippings.Domain.Entities;
using Shared.Domain.ValueObjects;

namespace Modules.Shippings.Application.Dtos
{
    public record ShippingMethodDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MoneyValue BasePrice { get; set; }
        public int DurationTimeInDays { get; set; }
        public static ShippingMethodDto CreateDtoFromObject(ShippingMethod shippingMethod)
        {
            return new ShippingMethodDto()
            {
                Id = shippingMethod.Id,
                Name = shippingMethod.Name,
                BasePrice = shippingMethod.BasePrice,
                DurationTimeInDays = shippingMethod.DurationTimeInDays
            };
        }
    }
}
