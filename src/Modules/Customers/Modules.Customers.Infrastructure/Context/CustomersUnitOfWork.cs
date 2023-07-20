using Modules.Customers.Application.Contracts;
using Shared.Abstractions.DomainEvents;
using Shared.Abstractions.UnitOfWork;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Customers.Infrastructure.Context
{    
    internal class CustomersUnitOfWork : SqlServerUnitOfWork<CustomersDbContext>, ICustomersUnitOfWork
    {
        public CustomersUnitOfWork(CustomersDbContext dbContext,
                                   IDomainEventDispatcher domainEventDispatcher) 
            : base(dbContext, domainEventDispatcher)
        {
        }
    }
}
