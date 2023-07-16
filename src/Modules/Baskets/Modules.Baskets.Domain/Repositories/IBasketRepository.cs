using Modules.Baskets.Domain.Entities;
using Modules.Baskets.Domain.ValueObjects;

namespace Modules.Baskets.Domain.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket> CreateBasket(Basket basket);
        Task<Basket> GetBasketByCustomerId(BasketCustomerId customerId);
        Task RemoveItem(Basket basket, BasketItemId basketItemId);
        void DeleteBasket(Basket basket);
    }
}
