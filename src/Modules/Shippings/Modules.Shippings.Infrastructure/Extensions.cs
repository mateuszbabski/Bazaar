﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Shippings.Application.Contracts;
using Modules.Shippings.Contracts;
using Modules.Shippings.Domain.Repositories;
using Modules.Shippings.Infrastructure.Context.ShippingMethods;
using Modules.Shippings.Infrastructure.Context.Shippings;
using Modules.Shippings.Infrastructure.Repository;

namespace Modules.Shippings.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddShippingsInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShippingsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IShippingsUnitOfWork, ShippingsUnitOfWork>();
            services.AddScoped<IShippingRepository, ShippingRepository>();

            services.AddDbContext<ShippingMethodsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IShippingMethodsUnitOfWork, ShippingMethodsUnitOfWork>();
            services.AddScoped<IShippingMethodRepository, ShippingMethodRepository>();
            services.AddScoped<IShippingMethodChecker, ShippingMethodRepository>();

            return services;
        }
    }
}
