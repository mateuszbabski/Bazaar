using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Behaviors;
using Shared.Application.Middleware;

namespace Shared.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddSharedApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<GlobalExceptionHandlerMiddleware>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

            return services;
        }
    }
}
