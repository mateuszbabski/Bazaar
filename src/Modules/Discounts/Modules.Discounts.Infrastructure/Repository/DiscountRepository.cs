using Microsoft.EntityFrameworkCore;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using Modules.Discounts.Domain.ValueObjects;
using Modules.Discounts.Infrastructure.Context;

namespace Modules.Discounts.Infrastructure.Repository
{
    internal sealed class DiscountRepository : IDiscountRepository
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

        public async Task<IEnumerable<Discount>> GetAll()
        {
            //return await _dbContext.Discounts.ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<Discount> GetDiscountById(DiscountId id)
        {
            //return await _dbContext.Discounts.FirstOrDefaultAsync(x => x.Id == id);
            throw new NotImplementedException();
        }
    }
}
