using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;

namespace Modules.Discounts.Domain.Repositories
{
    public interface IDiscountCouponRepository
    {
        Task<DiscountCoupon> Add(DiscountCoupon discountCoupon);
        Task<DiscountCoupon> GetDiscountCouponById(DiscountCouponId id);
        Task<DiscountCoupon> GetDiscountCouponByCouponCode(string couponCode);
        Task<IEnumerable<DiscountCoupon>> GetAll();
        Task<IEnumerable<DiscountCoupon>> GetAllByCreator(Guid id);
        Task<IEnumerable<DiscountCoupon>> GetAllTargetedForCustomer(Guid id);
        Task<IEnumerable<DiscountCoupon>> GetAllByDiscountId(DiscountId discountId);
    }
}
