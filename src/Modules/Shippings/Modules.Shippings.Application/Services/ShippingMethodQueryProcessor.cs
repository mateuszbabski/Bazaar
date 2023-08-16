using Modules.Shippings.Domain.Entities;
using Shared.Application.Queries;
using System.Linq.Expressions;

namespace Modules.Shippings.Application.Services
{
    internal class ShippingMethodQueryProcessor : QueryProcessor<ShippingMethod>
    {
        public override IQueryable<ShippingMethod> SortQuery(IQueryable<ShippingMethod> baseQuery, string sortBy, string sortDirection)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<ShippingMethod, object>>>(StringComparer.OrdinalIgnoreCase)
                {
                    { nameof(ShippingMethod.Name), r => r.Name.Value },
                    { nameof(ShippingMethod.BasePrice.Amount), r => r.BasePrice.Amount },
                    { nameof(ShippingMethod.DurationTimeInDays), r => r.DurationTimeInDays }
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
