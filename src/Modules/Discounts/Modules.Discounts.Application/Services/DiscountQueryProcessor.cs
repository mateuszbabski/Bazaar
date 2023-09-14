using Modules.Discounts.Domain.Entities;
using Shared.Application.Queries;
using System.Linq.Expressions;

namespace Modules.Discounts.Application.Services
{
    internal class DiscountQueryProcessor : QueryProcessor<Discount>
    {
        public override IQueryable<Discount> SortQuery(IQueryable<Discount> baseQuery, string sortBy, string sortDirection)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Discount, object>>>(StringComparer.OrdinalIgnoreCase)
                {
                    { nameof(Discount.DiscountTarget.DiscountType), r => r.DiscountTarget.DiscountType },
                    { nameof(Discount.IsPercentageDiscount), r => r.IsPercentageDiscount },
                    { nameof(Discount.DiscountValue), r => r.DiscountValue }
                };

                if (columnsSelector.TryGetValue(sortBy, out var selectedColumn))
                {
                    baseQuery = sortDirection == "ASC"
                            ? baseQuery.OrderBy(selectedColumn)
                            : baseQuery.OrderByDescending(selectedColumn);
                }
                else
                {
                    return baseQuery;
                }
            }

            return baseQuery;
        }
    }
}
