using Modules.Products.Infrastructure.Context;
using Shared.Infrastructure.DomainEvents;

namespace Modules.Products.Infrastructure.DomainEvents
{
    internal class ProductsDomainEventsAccessor : DomainEventsAccessor<ProductsDbContext>
    {
        public ProductsDomainEventsAccessor(ProductsDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
