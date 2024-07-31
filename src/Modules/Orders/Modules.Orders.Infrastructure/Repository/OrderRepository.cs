using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Context;

namespace Modules.Orders.Infrastructure.Repository
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly OrdersDbContext _dbContext;

        public OrderRepository(OrdersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Order> Add(Order order)
        {
            await _dbContext.Orders.AddAsync(order);

            return order;
        }
    }
}
