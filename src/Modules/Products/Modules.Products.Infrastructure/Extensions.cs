using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Products.Domain.Repositories;
using Modules.Products.Infrastructure.Context;
using Modules.Products.Infrastructure.Repository;
using Shared.Infrastructure.UnitOfWork;

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

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddUnitOfWork<ProductsUnitOfWork>();

            return services;
        }
    }
}
