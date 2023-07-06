using Microsoft.EntityFrameworkCore;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Repositories;
using Modules.Products.Domain.ValueObjects;
using Modules.Products.Infrastructure.Context;
using Shared.Domain.ValueObjects;

namespace Modules.Products.Infrastructure.Repository
{
    internal sealed class ProductRepository : IProductRepository
    {
        private readonly ProductsDbContext _dbContext;

        public ProductRepository(ProductsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> Add(Product product)
        {
            await _dbContext.Products.AddAsync(product);

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetById(ProductId id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            return await _dbContext.Products.Where(x => category == null 
                                                        || x.ProductCategory.CategoryName.ToLower().Contains(category.ToLower()))
                                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _dbContext.Products.Where(x => name == null
                                                        || x.ProductName.Value.ToLower().Contains(name.ToLower()))
                                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByShopId(ProductShopId shopId)
        {
            return await _dbContext.Products.Where(x => shopId == null
                                                        || x.ShopId == shopId)
                                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRange(decimal? minPrice, decimal? maxPrice)
        {
            return await _dbContext.Products.Where(x => minPrice == null
                                                        || x.Price.Amount >= minPrice)
                                            .Where(x => maxPrice == null
                                                        || x.Price.Amount <= maxPrice)
                                            .ToListAsync();
        }
    }
}
