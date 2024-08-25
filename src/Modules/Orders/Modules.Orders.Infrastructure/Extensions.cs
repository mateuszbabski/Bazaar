using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Orders.Application.Contracts;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Context;
using Modules.Orders.Infrastructure.Repository;

namespace Modules.Orders.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddOrdersInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrdersDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IOrdersUnitOfWork, OrdersUnitOfWork>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
