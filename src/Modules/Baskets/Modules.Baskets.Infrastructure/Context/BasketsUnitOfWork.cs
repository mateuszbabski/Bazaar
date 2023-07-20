using Modules.Baskets.Application.Contracts;
using Modules.Baskets.Domain.Repositories;
using Shared.Abstractions.DomainEvents;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Baskets.Infrastructure.Context
{
    internal class BasketsUnitOfWork : SqlServerUnitOfWork<BasketsDbContext>, IBasketsUnitOfWork
    {
        public BasketsUnitOfWork(BasketsDbContext dbContext,
                                 IDomainEventDispatcher domainEventDispatcher)
            : base(dbContext, domainEventDispatcher)
        {
        }
    }
}
