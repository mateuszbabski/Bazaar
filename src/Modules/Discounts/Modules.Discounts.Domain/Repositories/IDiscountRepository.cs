using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;

namespace Modules.Discounts.Domain.Repositories
{
    public interface IDiscountRepository
    {
        Task<Discount> Add(Discount discount);
        Task<Discount> GetDiscountById(DiscountId id);
        Task<IEnumerable<Discount>> GetAllCreatorDiscounts(Guid creatorId);
        Task<IEnumerable<Discount>> GetDiscountsByType(DiscountType discountType, Guid? discountTargetId);
        Task<Discount> GetDiscountByCouponCode(string couponCode);  
        Task<IEnumerable<Discount>> GetAll();
        void Delete(Discount discount);
    }
}
