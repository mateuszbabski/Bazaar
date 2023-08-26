using Modules.Discounts.Domain.Entities;
using Shared.Application.Queries;
using System.Linq.Expressions;

namespace Modules.Discounts.Application.Services
{
    internal class DiscountCouponQueryProcessor : QueryProcessor<DiscountCoupon>
    {
        public override IQueryable<DiscountCoupon> SortQuery(IQueryable<DiscountCoupon> baseQuery, string sortBy, string sortDirection)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<DiscountCoupon, object>>>(StringComparer.OrdinalIgnoreCase)
                {
                    { nameof(DiscountCoupon.StartsAt), r => r.StartsAt },
                    { nameof(DiscountCoupon.ExpirationDate), r => r.ExpirationDate },
                    { nameof(DiscountCoupon.IsEnable), r => r.IsEnable }
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
