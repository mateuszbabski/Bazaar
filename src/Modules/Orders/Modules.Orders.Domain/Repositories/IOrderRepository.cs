using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> Add(Order order);
    }
}
