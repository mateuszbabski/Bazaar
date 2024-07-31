using Shared.Application;
using Shared.Infrastructure;
using Modules.Customers.Api;
using Modules.Shops.Api;
using Modules.Products.Api;
using Shared.Infrastructure.Modules;
using System.Reflection;
using Modules.Baskets.Api;
using Modules.Shippings.Api;
using Modules.Discounts.Api;
using Modules.Orders.Api;

namespace Bootstrapper
{
    public static class Extensions
    {
        public static IServiceCollection AddModules(this IServiceCollection services,
                                                        IConfiguration configuration)
        {
            var assemblies = ModuleLoader.LoadAssemblies(configuration, "Bazaar.Modules.");
            var modules = ModuleLoader.LoadModules(assemblies);

            var arrayAssemblies = assemblies.ToArray<Assembly>();

            services.AddSharedInfrastructure(configuration, arrayAssemblies);
            services.AddSharedApplication();   

            services.AddCustomersModule(configuration);
            services.AddShopsModule(configuration);
            services.AddProductsModule(configuration);
            services.AddBasketsModule(configuration);
            services.AddShippingsModule(configuration);
            services.AddDiscountsModule(configuration);
            services.AddOrdersModule(configuration);

            return services;
        }
    }
}
