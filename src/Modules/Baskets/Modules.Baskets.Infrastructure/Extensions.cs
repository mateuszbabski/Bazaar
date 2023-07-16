using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Baskets.Domain.Repositories;
using Modules.Baskets.Infrastructure.Context;
using Modules.Baskets.Infrastructure.Repository;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Baskets.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddBasketsInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BasketsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddUnitOfWork<BasketsUnitOfWork>();

            return services;
        }
    }
}
