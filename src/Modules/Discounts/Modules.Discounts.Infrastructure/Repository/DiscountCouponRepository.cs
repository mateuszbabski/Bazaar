using Microsoft.EntityFrameworkCore;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using Modules.Discounts.Domain.ValueObjects;
using Modules.Discounts.Infrastructure.Context;

namespace Modules.Discounts.Infrastructure.Repository
{
    internal sealed class DiscountCouponRepository : IDiscountCouponRepository
    {// TODO: Repository for discounts
        private readonly DiscountsDbContext _dbContext;

        public DiscountCouponRepository(DiscountsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<DiscountCoupon> Add(DiscountCoupon discountCoupon)
        {
            await _dbContext.DiscountCoupons.AddAsync(discountCoupon);
            return discountCoupon;
        }

        public async Task<IEnumerable<DiscountCoupon>> GetAll()
        {
            //return await _dbContext.DiscountCoupons.ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<DiscountCoupon> GetDiscountCouponById(DiscountCouponId id)
        {
            //return await _dbContext.DiscountCoupons.FirstOrDefaultAsync(x => x.Id == id);
            throw new NotImplementedException();
        }
    }
}
