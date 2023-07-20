using Shared.Abstractions.DomainEvents;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Baskets.Infrastructure.Context
{
    internal class BasketsUnitOfWork : SqlServerUnitOfWork<BasketsDbContext>
    {
        public BasketsUnitOfWork(BasketsDbContext dbContext,
                                 IDomainEventDispatcher domainEventDispatcher)
            : base(dbContext, domainEventDispatcher)
        {
        }
    }
}
