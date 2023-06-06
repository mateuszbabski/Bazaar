using Shared.Application;
using Shared.Infrastructure;
using Modules.Customers.Api;

namespace Bootstrapper
{
    public static class Extensions
    {
        public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomersModule(configuration);

            services.AddSharedInfrastructure(configuration);
            services.AddSharedApplication(configuration);   

            return services;
        }
    }
}
