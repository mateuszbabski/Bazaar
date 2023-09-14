using Microsoft.EntityFrameworkCore;
using Modules.Baskets.Domain.Entities;
using Modules.Baskets.Domain.Repositories;
using Modules.Baskets.Domain.ValueObjects;
using Modules.Baskets.Infrastructure.Context;

namespace Modules.Baskets.Infrastructure.Repository
{
    internal sealed class BasketRepository : IBasketRepository
    {
        private readonly BasketsDbContext _dbContext;

        public BasketRepository(BasketsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Basket> CreateBasket(Basket basket)
        {
            await _dbContext.Baskets.AddAsync(basket);

            return basket;
        }

        public void DeleteBasket(Basket basket)
        {
            _dbContext.Baskets.Remove(basket);
        }

        public async Task<Basket> GetBasketByCustomerId(Guid customerId)
        {
            return await _dbContext.Baskets.Include(x => x.Items)
                                           .FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }

        public async Task RemoveItem(Basket basket, BasketItemId basketItemId)
        {
            var product = await _dbContext.BasketItems.Where(x => x.BasketId == basket.Id)
                                                      .FirstOrDefaultAsync(x => x.Id == basketItemId);

            _dbContext.BasketItems.Remove(product);

            if (basket.Items.Count == 0)
            {
                _dbContext.Baskets.Remove(basket);
            }
        }
    }
}
