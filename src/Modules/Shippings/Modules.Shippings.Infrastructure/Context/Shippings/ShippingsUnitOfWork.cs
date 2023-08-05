using Modules.Shippings.Application.Contracts;
using Shared.Abstractions.DomainEvents;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Shippings.Infrastructure.Context.Shippings
{
    internal class ShippingsUnitOfWork : SqlServerUnitOfWork<ShippingsDbContext>, IShippingsUnitOfWork
    {
        public ShippingsUnitOfWork(ShippingsDbContext dbContext,
                                   IDomainEventDispatcher domainEventDispatcher)
            : base(dbContext, domainEventDispatcher)
        {

        }
    }
}
