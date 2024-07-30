using Modules.Discounts.Domain.Entities;

namespace Modules.Discounts.Contracts.Interfaces
{
    public interface IDiscountChecker
    {
        Task<Discount> GetDiscountByCouponCodeToProcess(string couponCode);
    }
}
