using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;

namespace Modules.Discounts.Domain.Repositories
{
    public interface IDiscountRepository
    {
        Task<Discount> Add(Discount discount);
        Task<Discount> GetDiscountById(DiscountId id);
        Task<IEnumerable<Discount>> GetAll();
    }
}
