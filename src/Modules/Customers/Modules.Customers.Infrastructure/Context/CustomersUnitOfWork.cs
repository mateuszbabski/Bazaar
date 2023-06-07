using Shared.Abstractions.DomainEvents;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Customers.Infrastructure.Context
{
    internal class CustomersUnitOfWork : SqlServerUnitOfWork<CustomersDbContext>
    {
        public CustomersUnitOfWork(CustomersDbContext dbContext,
                                   IDomainEventDispatcher domainEventDispatcher) 
            : base(dbContext, domainEventDispatcher)
        {
        }
    }
}
