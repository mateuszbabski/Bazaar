using Microsoft.EntityFrameworkCore;
using Modules.Products.Contracts.Interfaces;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Repositories;
using Modules.Products.Domain.ValueObjects;
using Modules.Products.Infrastructure.Context;

namespace Modules.Products.Infrastructure.Repository
{
    internal sealed class ProductRepository : IProductRepository, IProductChecker
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
            if (String.IsNullOrEmpty(name))
            {
                return await _dbContext.Products.ToListAsync();
            }

            var allProducts = await _dbContext.Products.ToListAsync();
            var filteredProductList = allProducts.Where(x => x.ProductName.Value.ToLower().Contains(name.ToLower()));

            return filteredProductList;
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

        public async Task<bool> IsProductExisting(Guid userId, Guid? discountTargetId)
        {
            var product = await GetById(discountTargetId);
            return product.ShopId.Value == userId;
        }
    }
}
