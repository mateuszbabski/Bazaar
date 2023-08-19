using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using Modules.Discounts.Domain.ValueObjects;

namespace Modules.Discounts.Infrastructure.Repository
{
    internal sealed class DiscountRepository : IDiscountRepository
    {
        public Task<Discount> Add(Discount discount)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Discount>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Discount> GetDiscountById(DiscountId id)
        {
            throw new NotImplementedException();
        }
    }
}
