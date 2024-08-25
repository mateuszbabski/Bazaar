using Modules.Discounts.Domain.Entities;

namespace Modules.Discounts.Contracts.Interfaces
{
    public interface IDiscountCouponChecker
    {
        Task<DiscountCoupon> GetDiscountCouponByCodeToProcess(string couponCode);
    }
}
