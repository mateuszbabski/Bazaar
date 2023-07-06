using Shared.Abstractions.DomainEvents;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Products.Infrastructure.Context
{
    internal class ProductsUnitOfWork : SqlServerUnitOfWork<ProductsDbContext>
    {
        public ProductsUnitOfWork(ProductsDbContext dbContext,
                               IDomainEventDispatcher domainEventDispatcher)
            : base(dbContext, domainEventDispatcher)
        {
        }
    }
}
