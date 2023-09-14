using Microsoft.EntityFrameworkCore;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using Modules.Discounts.Domain.ValueObjects;
using Modules.Discounts.Infrastructure.Context;

namespace Modules.Discounts.Infrastructure.Repository
{
    internal sealed class DiscountCouponRepository : IDiscountCouponRepository
    {
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

        public async Task<DiscountCoupon> GetDiscountCouponByCouponCode(string couponCode)
        {
            return await _dbContext.DiscountCoupons.FirstOrDefaultAsync(x => x.DiscountCode == couponCode);
        }

        public async Task<DiscountCoupon> GetDiscountCouponById(DiscountCouponId id)
        {
            return await _dbContext.DiscountCoupons.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<DiscountCoupon>> GetAll()
        {
            return await _dbContext.DiscountCoupons.ToListAsync();
        }

        public async Task<IEnumerable<DiscountCoupon>> GetAllByCreator(Guid id)
        {
            return await _dbContext.DiscountCoupons.Where(x => x.CreatedBy == id)
                                                   .ToListAsync();
        }

        public async Task<IEnumerable<DiscountCoupon>> GetAllByDiscountId(DiscountId discountId)
        {
            return await _dbContext.DiscountCoupons.Where(x => x.DiscountId == discountId)
                                                   .ToListAsync();
        }

        public async Task<IEnumerable<DiscountCoupon>> GetAllTargetedForCustomer(Guid customerId)
        {
            var discountCoupons = await _dbContext.Discounts.Include(x => x.DiscountCoupons)
                                                            .Where(x => x.DiscountTarget.TargetId == customerId)
                                                            .SelectMany(x => x.DiscountCoupons)
                                                            .ToListAsync();
            return discountCoupons;
        }
    }
}
