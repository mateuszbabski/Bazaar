using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Discounts.Application.Contracts;
using Modules.Discounts.Contracts.Interfaces;
using Modules.Discounts.Domain.Repositories;
using Modules.Discounts.Infrastructure.Context;
using Modules.Discounts.Infrastructure.Repository;

namespace Modules.Discounts.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddDiscountsInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DiscountsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IDiscountsUnitOfWork, DiscountsUnitOfWork>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IDiscountChecker, DiscountRepository>();
            services.AddScoped<IDiscountCouponRepository, DiscountCouponRepository>();
            services.AddScoped<IDiscountCouponChecker, DiscountCouponRepository>();

            return services;
        }
    }
}
