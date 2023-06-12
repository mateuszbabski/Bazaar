using Modules.Customers.Infrastructure.Context;
using Shared.Infrastructure.DomainEvents;

namespace Modules.Customers.Infrastructure.DomainEvents
{
    internal class CustomersDomainEventsAccessor : DomainEventsAccessor<CustomersDbContext>
    {
        public CustomersDomainEventsAccessor(CustomersDbContext dbContext)
            :base(dbContext)
        {            
        }
    }
}
