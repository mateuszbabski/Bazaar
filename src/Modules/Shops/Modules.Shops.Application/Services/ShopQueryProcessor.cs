using Modules.Shops.Domain.Entities;
using Shared.Application.Queries;
using System.Linq.Expressions;

namespace Modules.Shops.Application.Services
{
    internal class ShopQueryProcessor : QueryProcessor<Shop>
    {
        public override IQueryable<Shop> SortQuery(IQueryable<Shop> baseQuery, string sortBy, string sortDirection)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Shop, object>>>(StringComparer.OrdinalIgnoreCase)
                {
                    { nameof(Shop.ShopName), r => r.ShopName.Value },
                    { nameof(Shop.ShopAddress.Country), r => r.ShopAddress.Country },
                    { nameof(Shop.ShopAddress.City), r => r.ShopAddress.City },
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
