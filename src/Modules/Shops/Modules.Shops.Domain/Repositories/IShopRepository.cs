using Modules.Shops.Domain.Entities;
using Modules.Shops.Domain.ValueObjects;

namespace Modules.Shops.Domain.Repositories
{
    public interface IShopRepository
    {
        Task<Shop> Add(Shop shop);
        Task<Shop> GetShopByEmail(string email);
        Task<Shop> GetShopById(ShopId id);
        Task<IEnumerable<Shop>> GetShopsByName(string name);
        Task<IEnumerable<Shop>> GetShopsByLocalization(string country, string city);
        Task<IEnumerable<Shop>> GetAllShops();
        Task Commit();
    }
}
