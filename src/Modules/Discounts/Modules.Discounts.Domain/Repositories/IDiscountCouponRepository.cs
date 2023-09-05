using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;

namespace Modules.Discounts.Domain.Repositories
{
    public interface IDiscountCouponRepository
    {
        Task<DiscountCoupon> Add(DiscountCoupon discountCoupon);
        Task<DiscountCoupon> GetDiscountCouponById(DiscountCouponId id);
        Task<DiscountCoupon> GetDiscountCouponCode(DiscountCode couponCode);
        Task<IEnumerable<DiscountCoupon>> GetAll();
        Task<IEnumerable<DiscountCoupon>> GetAllByDiscountId(DiscountId discountId);
    }
}
