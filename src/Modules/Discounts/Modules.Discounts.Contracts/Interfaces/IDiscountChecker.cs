namespace Modules.Discounts.Contracts.Interfaces
{
    public interface IDiscountChecker
    {
        Task<bool> IsDiscountExisting(Guid discountCouponId);
    }
}
