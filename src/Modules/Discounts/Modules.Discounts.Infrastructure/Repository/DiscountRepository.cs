using Microsoft.EntityFrameworkCore;
using Modules.Discounts.Contracts.Interfaces;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using Modules.Discounts.Domain.ValueObjects;
using Modules.Discounts.Infrastructure.Context;

namespace Modules.Discounts.Infrastructure.Repository
{
    internal sealed class DiscountRepository : IDiscountRepository, IDiscountChecker
    {
        private readonly DiscountsDbContext _dbContext;

        public DiscountRepository(DiscountsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Discount> Add(Discount discount)
        {
            await _dbContext.Discounts.AddAsync(discount);
            return discount;
        }

        public void Delete(Discount discount)
        {
            _dbContext.Discounts.Remove(discount);
        }

        public async Task<IEnumerable<Discount>> GetAll()
        {
            return await _dbContext.Discounts.ToListAsync();            
        }

        public async Task<IEnumerable<Discount>> GetAllCreatorDiscounts(Guid creatorId)
        {
            return await _dbContext.Discounts.Where(x => x.CreatedBy == creatorId)
                                             .ToListAsync();
        }

        public async Task<Discount> GetDiscountByCouponCode(string couponCode)
        {
            //var coupon = await _dbContext.DiscountCoupons
            //                             .Where(x => x.DiscountCode.Value == couponCode)
            //                             .FirstOrDefaultAsync();

            //return await _dbContext.Discounts.FirstOrDefaultAsync(x => x.Id == coupon.DiscountId);
            return await _dbContext.Discounts.Include(x => x.DiscountCoupons.Find(c => c.DiscountCode == couponCode))
                                             .FirstOrDefaultAsync();
        }

        public async Task<Discount> GetDiscountById(DiscountId id)
        {
            return await _dbContext.Discounts.FirstOrDefaultAsync(x => x.Id == id);            
        }

        public async Task<IEnumerable<Discount>> GetDiscountsByType(DiscountType discountType, Guid? discountTargetId)
        {
            return await _dbContext.Discounts.Where(x => x.DiscountTarget.DiscountType == discountType)
                                             .Where(x => discountTargetId == null 
                                                    || x.DiscountTarget.TargetId == discountTargetId)
                                             .ToListAsync();
        }

        public async Task<Discount> GetDiscountByCouponCodeToProcess(string couponCode)
        {
            return await GetDiscountByCouponCode(couponCode);
        }
    }
}
