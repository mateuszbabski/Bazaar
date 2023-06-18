using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Shops.Domain.Repositories;
using Modules.Shops.Infrastructure.Context;
using Modules.Shops.Infrastructure.DomainEvents;
using Modules.Shops.Infrastructure.Repository;
using Shared.Infrastructure.DomainEvents;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Shops.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddShopsInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShopsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddDomainEventsAccessor<ShopsDomainEventsAccessor>();
            services.AddUnitOfWork<ShopsUnitOfWork>();

            return services;
        }
    }
}
