using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddSharedApplication(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
