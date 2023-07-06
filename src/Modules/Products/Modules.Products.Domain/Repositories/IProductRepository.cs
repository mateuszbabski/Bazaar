using Modules.Products.Domain.Entities;
using Modules.Products.Domain.ValueObjects;

namespace Modules.Products.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> Add(Product product);
        Task<Product> GetById(ProductId id);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategory(string category);
        Task<IEnumerable<Product>> GetProductsByShopId(ProductShopId shopId);
        Task<IEnumerable<Product>> GetProductsByPriceRange(decimal? minPrice, decimal? maxPrice);
    }
}
