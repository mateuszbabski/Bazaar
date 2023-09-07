using Modules.Discounts.Domain.Entities;

namespace Modules.Discounts.Application.Dtos
{
    public record DiscountCouponDto
    {
        public Guid Id { get; init; }
        public Guid DiscountId { get; init; }
        public string DiscountCode { get;  init; }
        public DateTimeOffset StartsAt { get; init; }
        public DateTimeOffset ExpirationDate { get; init; }
        public bool IsEnable { get; init; }

        internal static IEnumerable<DiscountCouponDto> CreateDtoFromObjects(List<DiscountCoupon> discountCoupons)
        {
            var discountCouponList = new List<DiscountCouponDto>();

            foreach (var discountCoupon in discountCoupons)
            {
                var discountCouponDto = new DiscountCouponDto()
                {
                    Id = discountCoupon.Id,
                    DiscountId = discountCoupon.DiscountId,
                    DiscountCode = discountCoupon.DiscountCode,
                    StartsAt = DateTimeOffset.Now,
                    ExpirationDate = DateTimeOffset.Now,
                    IsEnable = discountCoupon.IsEnable
                };

                discountCouponList.Add(discountCouponDto);
            }

            return discountCouponList;
        }

        internal static DiscountCouponDto CreateDtoFromObject(DiscountCoupon discountCoupon)
        {
            var discountCouponDto = new DiscountCouponDto()
            {
                Id = discountCoupon.Id,
                DiscountId = discountCoupon.DiscountId,
                DiscountCode = discountCoupon.DiscountCode,
                StartsAt = DateTimeOffset.Now,
                ExpirationDate = DateTimeOffset.Now,
                IsEnable = discountCoupon.IsEnable
            };

            return discountCouponDto;
        }
    }
}
