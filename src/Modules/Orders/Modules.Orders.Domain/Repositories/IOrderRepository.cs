using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderById(OrderId id);
        Task<Order> Add(Order order);
    }
}
