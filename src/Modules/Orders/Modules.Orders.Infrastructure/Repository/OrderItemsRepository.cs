using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Context;

namespace Modules.Orders.Infrastructure.Repository
{
    internal class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly OrdersDbContext _dbContext;

        public OrderItemsRepository(OrdersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderItem> Add(OrderItem orderItem)
        {
            await _dbContext.OrderItems.AddAsync(orderItem);

            return orderItem;
        }

        public async Task<IEnumerable<OrderItem>> AddRange(IEnumerable<OrderItem> orderItems)
        {
            await _dbContext.OrderItems.AddRangeAsync(orderItems);

            return orderItems;
        }
    }
}
