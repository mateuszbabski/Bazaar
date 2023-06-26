using Microsoft.EntityFrameworkCore;
using Modules.Shops.Domain.Entities;
using Modules.Shops.Domain.Repositories;
using Modules.Shops.Domain.ValueObjects;
using Modules.Shops.Infrastructure.Context;
using Shared.Domain.ValueObjects;

namespace Modules.Shops.Infrastructure.Repository
{
    internal sealed class ShopRepository : IShopRepository
    {
        private readonly ShopsDbContext _dbContext;

        public ShopRepository(ShopsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Shop> Add(Shop shop)
        {
            await _dbContext.Shops.AddAsync(shop);

            return shop;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Shop?> GetShopByEmail(string email)
        {
            return await _dbContext.Shops.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Shop?> GetShopById(ShopId id)
        {
            return await _dbContext.Shops.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Shop?>> GetAllShops()
        {
            return await _dbContext.Shops.OrderBy(x => x.ShopName)
                                         .ToListAsync();
        }

        public async Task<IEnumerable<Shop>> GetShopsByName(string name)
        {
            return await _dbContext.Shops.Where(x => name == null
                                                     || x.ShopName.Value.ToLower().Contains(name.ToLower()))
                                         .OrderBy(x => x.ShopName)
                                         .ToListAsync();
        }

        public async Task<IEnumerable<Shop>> GetShopsByLocalization(string country, string city)
        {
            return await _dbContext.Shops.Where(x => country == null
                                                     || x.ShopAddress.Country.ToLower().Contains(country.ToLower()))
                                         .Where(x => city == null
                                                     || x.ShopAddress.City.ToLower().Contains(city.ToLower()))
                                         .OrderBy(x => x.ShopName)
                                         .ToListAsync();
        }
    }
}
