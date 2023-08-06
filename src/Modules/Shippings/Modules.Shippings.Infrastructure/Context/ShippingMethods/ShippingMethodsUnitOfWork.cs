using Modules.Shippings.Application.Contracts;
using Shared.Abstractions.DomainEvents;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Shippings.Infrastructure.Context.ShippingMethods
{
    internal class ShippingMethodsUnitOfWork : SqlServerUnitOfWork<ShippingMethodsDbContext>, IShippingMethodsUnitOfWork
    {
        public ShippingMethodsUnitOfWork(ShippingMethodsDbContext dbContext,
                                         IDomainEventDispatcher domainEventDispatcher)
            : base(dbContext, domainEventDispatcher)
        {

        }
    }
}
