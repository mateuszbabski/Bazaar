using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Domain.Repositories
{
    public interface IOrderItemsRepository
    {
        Task<IEnumerable<OrderItem>> AddRange(IEnumerable<OrderItem> orderItems);
        Task<OrderItem> Add(OrderItem orderItem);
    }
}
