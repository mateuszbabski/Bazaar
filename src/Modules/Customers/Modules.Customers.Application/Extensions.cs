using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Customers.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomersApplication(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
            
    }
}
