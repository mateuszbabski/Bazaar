using Modules.Shops.Application.Contracts;
using Shared.Abstractions.DomainEvents;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Shops.Infrastructure.Context
{
    internal class ShopsUnitOfWork : SqlServerUnitOfWork<ShopsDbContext>, IShopsUnitOfWork
    {
        public ShopsUnitOfWork(ShopsDbContext dbContext,
                               IDomainEventDispatcher domainEventDispatcher)
            : base(dbContext, domainEventDispatcher)
        {
        }
    }
}
