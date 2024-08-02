using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;

namespace Modules.Discounts.Contracts.Interfaces
{
    public interface IDiscountChecker
    {
        Task<Discount> GetDiscountByIdToProcess(DiscountId id);
    }
}
