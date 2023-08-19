using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;

namespace Modules.Discounts.Domain.Repositories
{
    public interface IDiscountCouponRepository
    {
        Task<DiscountCoupon> Add(DiscountCoupon discountCoupon);
        Task<DiscountCoupon> GetDiscountCouponById(DiscountCouponId id);
        Task<IEnumerable<DiscountCoupon>> GetAll();
    }
}
