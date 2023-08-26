using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Products.Application.Contracts;
using Modules.Products.Contracts.Interfaces;
using Modules.Products.Domain.Repositories;
using Modules.Products.Infrastructure.Context;
using Modules.Products.Infrastructure.Repository;

namespace Modules.Products.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddProductsInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IProductsUnitOfWork, ProductsUnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductChecker, ProductRepository>();

            return services;
        }
    }
}
