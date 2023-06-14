using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Middleware;

namespace Shared.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddSharedApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<GlobalExceptionHandlerMiddleware>();
            
            return services;
        }
    }
}
