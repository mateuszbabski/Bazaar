using Shared.Abstractions.DomainEvents;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Shops.Infrastructure.Context
{
    internal class ShopsUnitOfWork : SqlServerUnitOfWork<ShopsDbContext>
    {
        public ShopsUnitOfWork(ShopsDbContext dbContext,
                               IDomainEventDispatcher domainEventDispatcher)
            : base(dbContext, domainEventDispatcher)
        {
        }
    }
}
