using Modules.Discounts.Application.Contracts;
using Shared.Abstractions.DomainEvents;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Discounts.Infrastructure.Context
{
    internal class DiscountsUnitOfWork : SqlServerUnitOfWork<DiscountsDbContext>, IDiscountsUnitOfWork
    {
        public DiscountsUnitOfWork(DiscountsDbContext dbContext,
                                  IDomainEventDispatcher domainEventDispatcher)
            : base(dbContext, domainEventDispatcher)
        {
        }
    }
}
