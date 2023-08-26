using Modules.Discounts.Domain.Entities;

namespace Modules.Discounts.Application.Dtos
{
    public record DiscountDto
    {
        public Guid Id { get; init; }
        public Guid CreatedBy { get; init; }
        public decimal DiscountValue { get; init; }
        public bool IsPercentageDiscount { get; init; }
        public string? Currency { get; init; }
        public string DiscountType { get; init; }
        public Guid? DiscountTarget { get; init; }

        internal static IEnumerable<DiscountDto> CreateDtoFromObjects(List<Discount> discounts)
        {
            var discountList = new List<DiscountDto>();

            foreach (var discount in discounts)
            {
                var discountDto = new DiscountDto()
                {
                    Id = discount.Id,
                    CreatedBy = discount.CreatedBy,
                    DiscountValue = discount.DiscountValue, 
                    IsPercentageDiscount = discount.IsPercentageDiscount,
                    Currency = discount.Currency,
                    DiscountType = discount.DiscountTarget.DiscountType.ToString(),
                    DiscountTarget = discount.DiscountTarget.TargetId
                };

                discountList.Add(discountDto);
            }

            return discountList;
        }
    }
}
