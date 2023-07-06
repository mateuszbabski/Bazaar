using Modules.Products.Domain.Entities;
using Shared.Application.Queries;
using System.Linq.Expressions;

namespace Modules.Products.Application.Services
{
    internal class ProductQueryProcessor : QueryProcessor<Product>
    {
        public override IQueryable<Product> SortQuery(IQueryable<Product> baseQuery, string sortBy, string sortDirection)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Product, object>>>(StringComparer.OrdinalIgnoreCase)
                {
                    { nameof(Product.ProductName), r => r.ProductName.Value },
                    { nameof(Product.ProductCategory), r => r.ProductCategory },
                    { nameof(Product.Price.Amount), r => r.Price.Amount },
                    { nameof(Product.WeightPerUnit), r => r.WeightPerUnit },
                    { nameof(Product.ShopId), r => r.ShopId },

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
