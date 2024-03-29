﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Products.Application;
using Modules.Products.Infrastructure;

namespace Modules.Products.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddProductsModule(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddProductsInfrastructure(configuration);
            services.AddProductsApplication();

            return services;
        }
    }
}
