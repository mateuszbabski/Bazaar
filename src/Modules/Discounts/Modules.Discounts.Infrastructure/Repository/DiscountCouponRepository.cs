using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using Modules.Discounts.Domain.ValueObjects;

namespace Modules.Discounts.Infrastructure.Repository
{
    internal sealed class DiscountCouponRepository : IDiscountCouponRepository
    {
        public Task<DiscountCoupon> Add(DiscountCoupon discountCoupon)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DiscountCoupon>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<DiscountCoupon> GetDiscountCouponById(DiscountCouponId id)
        {
            throw new NotImplementedException();
        }
    }
}
