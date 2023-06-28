using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Queries;

namespace Shared.Application.Queries
{
    public static class Extensions
    {
        public static IServiceCollection AddQueryProcessor(this IServiceCollection services)
        {
            services.AddScoped(typeof(IQueryProcessor<>), typeof(QueryProcessor<>));

            return services;
        }

        public static IServiceCollection AddQueryProcessor<TQueryProcessor, T>(this IServiceCollection services)
            where TQueryProcessor : class, IQueryProcessor<T>
            where T : class
        {
            services.AddScoped<IQueryProcessor<T>, TQueryProcessor>();
            services.AddScoped<TQueryProcessor>();
            using var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<QueryProcessorTypeRegistry>().Register<TQueryProcessor>();

            return services;
        }
    }
}
