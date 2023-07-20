using Modules.Products.Application.Contracts;
using Shared.Abstractions.DomainEvents;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Products.Infrastructure.Context
{
    internal class ProductsUnitOfWork : SqlServerUnitOfWork<ProductsDbContext>, IProductsUnitOfWork
    {
        public ProductsUnitOfWork(ProductsDbContext dbContext,
                               IDomainEventDispatcher domainEventDispatcher)
            : base(dbContext, domainEventDispatcher)
        {
        }
    }
}
