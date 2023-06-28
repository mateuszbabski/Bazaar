using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Behaviors;
using Shared.Application.Middleware;
using Shared.Application.Queries;

namespace Shared.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddSharedApplication(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandlerMiddleware>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

            services.AddSingleton(new QueryProcessorTypeRegistry());
            services.AddQueryProcessor();

            return services;
        }
    }
}
