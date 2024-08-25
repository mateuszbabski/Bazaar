using Modules.Orders.Application.Contracts;
using Shared.Abstractions.DomainEvents;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Orders.Infrastructure.Context
{
    internal class OrdersUnitOfWork : SqlServerUnitOfWork<OrdersDbContext>, IOrdersUnitOfWork
    {
        public OrdersUnitOfWork(OrdersDbContext dbContext,
                               IDomainEventDispatcher domainEventDispatcher)
            : base(dbContext, domainEventDispatcher)
        {
        }
    }
}
